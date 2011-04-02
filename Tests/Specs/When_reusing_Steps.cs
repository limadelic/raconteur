using System;
using System.Collections.Generic;
using Common;
using FluentSpec;
using MbUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Raconteur;
using Raconteur.Compilers;
using Raconteur.Generators;
using Raconteur.Helpers;
using Raconteur.IDE;
using Raconteur.Parsers;
using Uncommon;

namespace Specs
{
    [TestFixture]
    public class When_reusing_Steps
    {
        readonly Feature Feature = new Feature
        {
            StepDefinitions = StepDefinitions
        };

        static readonly List<string> DeclaredStepDefinitions = new List<string>
        {
            "StepDefinitions", "AnotherStepDefinitions", "StepDefinitionsInSameNamespace"
        };

        static readonly List<Type> StepDefinitions = new List<Type>
        {
            typeof(StepDefinitions), typeof(AnotherStepDefinitions), typeof(StepDefinitionsInSameNamespace)
        };

        FeatureItem FeatureItem;

        FeatureParserClass Parser;
        FeatureCompilerClass Compiler;
        
        #region setup

        dynamic backup;

        [SetUp]
        public void SetUp() 
        {
            SetUpFeatureItem();
            SetUpParser();
            SetUpCompiler();

            backup = Settings.StepDefinitions;
        }

        void SetUpFeatureItem() 
        {
            FeatureItem = Substitute.For<FeatureItem>();
            FeatureItem.Assembly.Returns("Common");
        }

        void SetUpParser() 
        {
            Parser = new FeatureParserClass 
            {
                ScenarioTokenizer = Substitute.For<ScenarioTokenizer>(),
            };
        }

        void SetUpCompiler() 
        {
            Compiler = new FeatureCompilerClass 
            {
                TypeResolver = Substitute.For<TypeResolver>()
            };

            Compiler.TypeResolver.TypeOf("StepDefinitions", "Common").Returns(typeof(StepDefinitions));
            Compiler.TypeResolver.TypeOf("AnotherStepDefinitions", "Common").Returns(typeof(AnotherStepDefinitions));
            Compiler.TypeResolver.TypeOf("StepDefinitionsInSameNamespace", "Common").Returns(typeof(StepDefinitionsInSameNamespace));
            Compiler.TypeResolver.TypeOf("StepDefinitionsInLibrary", "Library").Returns(typeof(StepDefinitionsInLibrary));
        }

        [TearDown]
        public void TearDown()
        {
            Settings.StepDefinitions = backup;
        }

        #endregion

        [Test]
        public void should_include_default_StepDefinitions_in_Feature()
        {
            Compiler.TypeResolver
                .TypeOf("FeatureName", "Common")
                .Returns(typeof(StepDefinitions));

            var Feature = Parser.FeatureFrom(new FeatureFile
            {
                Content = 
                @"
                    Feature: Feature Name
                "
            }, FeatureItem);

            Compiler.Compile(Feature, FeatureItem);

            Feature.DefaultStepDefinitions.ShouldBe(typeof(StepDefinitions));
        }

        [Test]
        public void should_find_the_StepDefinitions()
        {
            var Feature = Parser.FeatureFrom(new FeatureFile
            {
                Content = 
                @"
                    Feature: Name

                    using Step Definitions
                    using Another Step Definitions
                    using StepDefinitionsInSameNamespace
                "
            }, FeatureItem);
            
            Feature.DeclaredStepDefinitions.ShouldBe(DeclaredStepDefinitions);

            ObjectFactory.NewFeatureCompiler.Compile(Feature, FeatureItem);

            Feature.StepDefinitions.ShouldBe(StepDefinitions);
        }

        [Test]
        public void should_find_the_StepDefinitions_before_the_Scenarios()
        {
            var Feature = Parser.FeatureFrom(new FeatureFile
            {
                Content = 
                @"
                    Feature: Name

                    using Step Definitions

                    Scenario: Lola

                    using Another Step Definitions
                "
            }, FeatureItem);
            
            ObjectFactory.NewFeatureCompiler.Compile(Feature, FeatureItem);

            Feature.StepDefinitions.Count.ShouldBe(1);
        }

        [Test]
        public void should_include_the_StepsDefinitions_from_Settings()
        {
            Settings.StepDefinitions = new List<string> 
            {
                "StepDefinitions", "AnotherStepDefinitions", "StepDefinitionsInSameNamespace"
            };

            var Feature = Parser.FeatureFrom(new FeatureFile
            {
                Content = 
                @"
                    Feature: Name
                "
            }, FeatureItem);

            ObjectFactory.NewFeatureCompiler.Compile(Feature, FeatureItem);

            Feature.StepDefinitions.ShouldBe(StepDefinitions);
        }

        [Test]
        public void should_find_StepsDefinitions_in_Libraries()
        {
            Settings.Libraries = new List<string> { "Library" };

            var Feature = Parser.FeatureFrom(new FeatureFile
            {
                Content = 
                @"
                    Feature: Name

                    using Step Definitions in Library
                "
            }, FeatureItem);

            ObjectFactory.NewFeatureCompiler.Compile(Feature, FeatureItem);

            Feature.StepDefinitions.ShouldBe(new List<Type> { typeof(StepDefinitionsInLibrary) });
        }

        [Test]
        public void should_find_types_in_different_assemblies()
        {
            new TypeResolverClass()
                .TypeOf("StepDefinitions", "Common.dll")
                .Name.ShouldBe("StepDefinitions");
        }

        [Test]
        public void should_add_namespace_to_runner() 
        {
            new RunnerGenerator(Feature).Code.TrimLines().ShouldContain
            (@"
                using Common;
                using Uncommon;
            "
            .TrimLines());
        }

        [Test]
        public void should_not_duplicate_namespaces() 
        {
            new RunnerGenerator(Feature).Code.TrimLines().ShouldNotContain
            (@"
                using Common;
                using Uncommon;
                using Common;
            "
            .TrimLines());
        }

        [Test]
        public void should_declare_the_StepDefinitions()
        {
            new RunnerGenerator(Feature).Code.TrimLines().ShouldContain
            (@"
                public StepDefinitions StepDefinitions = new StepDefinitions();
                public AnotherStepDefinitions AnotherStepDefinitions = new AnotherStepDefinitions();
            "
            .TrimLines());
        }

        [Test]
        public void should_use_Steps_from_StepDefinitions()
        {
            new StepGenerator
            (
                new Step
                {
                    Name = "Step", 
                    Implementation = Common.StepDefinitions.StepMethod
                }
            )
            .Code.ShouldContain("StepDefinitions.Step();");

            new StepGenerator
            (
                new Step
                {
                    Name = "another_Step",
                    Implementation = AnotherStepDefinitions.AnotherStepMethod
                }
            )
            .Code.ShouldContain("AnotherStepDefinitions.another_Step();");
        }

        [Test]
        public void should_use_overloaded_Steps_from_StepDefinitions()
        {
            new StepGenerator
            (
                new Step
                {
                    Name = "Step", 
                    Implementation = Common.StepDefinitions.StepMethod
                }
            )
            .Code.ShouldContain("StepDefinitions.Step();");
        }

        [Test]
        public void should_consider_each_table_columns_as_arg_if_header()
        {
            var Feature = new Feature 
            { 
                DeclaredStepDefinitions = { "StepDefinitions" },
                Scenarios = { new Scenario { Steps = { new Step
                {
                    Name = "Step",
                    Table = new Table
                    {
                        HasHeader = true,
                        Rows =
                        {
                            new List<string> {"X","Y"}, 
                            new List<string> {"1","0"}
                        }
                    }
                }
            }}}};

            ObjectFactory.NewFeatureCompiler.Compile(Feature, FeatureItem);

            Feature.Steps[0].Implementation
                .ShouldBe(Common.StepDefinitions.StepWithTwoArgs);
        }

        [Test]
        public void should_resolve_method_overloading_by_Arg_count()
        {
            var Feature = new Feature 
            { 
                DeclaredStepDefinitions = { "StepDefinitions" },
                Scenarios = { new Scenario { Steps =
                {
                    new Step { Name = "Step", },
                    new Step { Name = "Step", Args = new List<string> { "Arg" }, }, 
                }
            }}};

            ObjectFactory.NewFeatureCompiler.Compile(Feature, FeatureItem);

            Feature.Steps[0].Implementation
                .ShouldBe(Common.StepDefinitions.StepMethod);

            Feature.Steps[1].Implementation
                .ShouldBe(Common.StepDefinitions.StepOverloaded);
        }

        [Test]
        [MbUnit.Framework.ExpectedException(typeof(AssertFailedException))]
        public void should_resolve_method_overloading_by_Arg_type()
        {
            var Feature = new Feature 
            { 
                DeclaredStepDefinitions = { "StepDefinitions" },
                Scenarios = { new Scenario { Steps =
                {
                    new Step { Name = "Step", Args = new List<string> { "42" }, },
                    new Step { Name = "Step", Args = new List<string> { "Arg" },}, 
                }
            }}};

            ObjectFactory.NewFeatureCompiler.Compile(Feature, FeatureItem);

            Feature.Steps[0].Implementation
                .ShouldBe(Common.StepDefinitions.StepOverloadedInt);

            Feature.Steps[1].Implementation
                .ShouldBe(Common.StepDefinitions.StepOverloaded);
        }

        [Test]
        public void should_format_args_according_the_declared_type_on_implementation()
        {
            new StepGenerator
            (
                new Step
                {
                    Name = "Step",
                    Args = { "42" },
                    Implementation = Common.StepDefinitions.StepOverloaded
                }
            )
            .Code.ShouldContain("StepDefinitions.Step(\"42\");");
        }

        [Test]
        [Row("default", typeof(object), "default")]
        [Row("string", typeof(string), "\"string\"")]
        [Row("1/1/77", typeof(DateTime), "System.DateTime.Parse(\"1/1/77\")")]
        public void should_format_args_according_types(string Arg, Type Type, string ExpectedFormat)
        {
            ArgFormatter.ValueOf(Arg, Type).ShouldBe(ExpectedFormat);
        }
    }
}