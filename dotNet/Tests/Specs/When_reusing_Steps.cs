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
        Feature Feature = new Feature
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

        readonly FeatureItem FeatureItem = Actors.FeatureItem("Common");

        FeatureParserClass Parser;
        FeatureCompilerClass Compiler;
        
        #region setup

        readonly List<dynamic> backup = new List<dynamic>();

        [SetUp]
        public void SetUp() 
        {
            backup.Add(Settings.StepDefinitions);
            backup.Add(Settings.Libraries);

            SetUpParser();
            SetUpCompiler();

            Settings.Libraries = new List<string> { "Common" };
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
            Settings.StepDefinitions = backup[0];
            Settings.Libraries = backup[1];
        }

        #endregion

        [Test]
        public void should_include_default_StepDefinitions_in_Feature() {
            Compiler.TypeResolver
                .TypeOf("FeatureName", "Common")
                .Returns(typeof(StepDefinitions));

            var Feature = Parser.FeatureFrom(new FeatureFile {
                Content =
                @"
                    Feature: Feature Name
                "
            }, FeatureItem);

            Compiler.Compile(Feature, FeatureItem);

            Feature.DefaultStepDefinitions.ShouldBe(typeof(StepDefinitions));
            Feature.StepDefinitions[0].ShouldBe(Feature.DefaultStepDefinitions);
        }

        [Test]
        public void should_find_the_StepDefinitions() {
            var Feature = Parser.FeatureFrom(new FeatureFile {
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
        public void should_find_the_StepDefinitions_before_the_Scenarios() {
            var Feature = Parser.FeatureFrom(new FeatureFile {
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
        public void should_include_the_StepsDefinitions_from_Settings() {
            Settings.StepDefinitions = new List<string> 
            {
                "StepDefinitions", "AnotherStepDefinitions", "StepDefinitionsInSameNamespace"
            };

            var Feature = Parser.FeatureFrom(new FeatureFile {
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
            var Feature = Parser.FeatureFrom(new FeatureFile
            {
                Content = 
                @"
                    Feature: Name

                    using Step Definitions in Library
                "
            }, FeatureItem);

            ObjectFactory.NewFeatureCompiler.Compile(Feature, FeatureItem);

            Feature.StepDefinitions[0].ShouldBe(typeof(StepDefinitionsInLibrary));
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
                using Uncommon;
                using Common;
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
        public void should_not_declare_the_DefaultStepDefinitions()
        {
            new RunnerGenerator(Feature).Code.TrimLines().ShouldNotContain
            (@"
                public StepDefinitions StepDefinitions = new StepDefinitions();
            "
            .TrimLines());
        }

        [Test]
        public void should_declare_the_StepDefinitions()
        {
            new RunnerGenerator(Feature).Code.TrimLines().ShouldContain
            (@"
                public AnotherStepDefinitions AnotherStepDefinitions = new AnotherStepDefinitions();
                public StepDefinitionsInSameNamespace StepDefinitionsInSameNamespace = new StepDefinitionsInSameNamespace();
            "
            .TrimLines());
        }

        [Test]
        public void should_use_Steps_from_StepDefinitions()
        {
            ObjectFactory.NewStepRunnerGenerator
            (
                new Step
                {
                    Name = "Step", 
                    Method = Common.StepDefinitions.StepMethod,
                    Feature = new Feature
                    {
                        StepDefinitions = {typeof(Feature), typeof(StepDefinitions)}
                    }
                }
            )
            .Code.ShouldContain("StepDefinitions.Step();");

            ObjectFactory.NewStepRunnerGenerator
            (
                new Step
                {
                    Name = "another_Step",
                    Method = AnotherStepDefinitions.AnotherStepMethod,
                    Feature = new Feature
                    {
                        StepDefinitions = {typeof(Feature), typeof(AnotherStepDefinitions)}
                    }
                }
            )
            .Code.ShouldContain("AnotherStepDefinitions.another_Step();");
        }

        [Test]
        public void should_use_overloaded_Steps_from_StepDefinitions()
        {
            ObjectFactory.NewStepRunnerGenerator
            (
                new Step
                {
                    Name = "Step", 
                    Method = Common.StepDefinitions.StepMethod,
                    Feature = new Feature
                    {
                        StepDefinitions = {typeof(Feature), typeof(StepDefinitions)}
                    }
                }
            )
            .Code.ShouldContain("StepDefinitions.Step();");
        }

        [Test]
        public void should_consider_each_table_columns_as_arg_if_header()
        {
            var Feature = new Feature 
            { 
                Name = "StepDefinitions",
                Scenarios = { new Scenario { Steps = { new Step
                {
                    Name = "Step",
                    Args = new List<string> { "Arg" },
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

            Feature.Steps[0].Method
                .ShouldBe(Common.StepDefinitions.StepWithThreeArgs);
        }

        [Test]
        public void should_find_Step_in_DefaultStepDefitions()
        {
            var Feature = new Feature 
            { 
                Name = "StepDefinitions",
                Scenarios = { new Scenario { Steps = { new Step { Name = "Step", } }
            }}};

            ObjectFactory.NewFeatureCompiler.Compile(Feature, FeatureItem);

            Feature.Steps[0].Method
                .ShouldBe(Common.StepDefinitions.StepMethod);
        }

        [Test]
        public void should_not_find_private_Steps_in_StepDefitions()
        {
            var Feature = new Feature 
            { 
                Name = "StepDefinitions",
                Scenarios = { new Scenario { Steps = { new Step { Name = "StepPrivate", } }
            }}};

            ObjectFactory.NewFeatureCompiler.Compile(Feature, FeatureItem);

            Feature.Steps[0].Method
                .ShouldBe(Common.StepDefinitions.PrivateStep);
        }

        [Test]
        public void should_find_private_Steps_in_other_DefaultStepDefitions()
        {
            var Feature = new Feature 
            { 
                Name = "FeatureName",
                Scenarios = { new Scenario { Steps = { new Step { Name = "StepPrivate", } }
            }}};

            ObjectFactory.NewFeatureCompiler.Compile(Feature, FeatureItem);

            Feature.Steps[0].Method.ShouldBeNull();
        }

        [Test]
        public void should_resolve_method_overloading_by_Arg_count()
        {
            var Feature = new Feature 
            { 
                DeclaredStepDefinitions = { "StepDefinitions" },
                Scenarios = { new Scenario { Steps =
                {
                    new Step { Name = "Step", Args = new List<string> { "Arg" }, }, 
                    new Step { Name = "Step", Args = new List<string> { "Arg1", "2", "\"Arg3\"" }, }, 
                }
            }}};
            
            ObjectFactory.NewFeatureCompiler.Compile(Feature, FeatureItem);

            Feature.Steps[0].Method
                .ShouldBe(Common.StepDefinitions.StepOverloaded);

            Feature.Steps[1].Method
                .ShouldBe(Common.StepDefinitions.StepWithThreeArgs);
        }

        [Test]
        [MbUnit.Framework.ExpectedException(typeof(AssertFailedException))]
        public void should_resolve_method_overloading_by_Arg_type()
        {
            typeof(string).IsArray.ShouldBeFalse();

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

            Feature.Steps[0].Method
                .ShouldBe(Common.StepDefinitions.StepOverloadedInt);

            Feature.Steps[1].Method
                .ShouldBe(Common.StepDefinitions.StepOverloaded);
        }

        [Test]
        public void should_format_args_according_the_declared_type_on_implementation()
        {
            ObjectFactory.NewStepRunnerGenerator
            (
                new Step
                {
                    Name = "Step",
                    Args = { "42" },
                    Method = Common.StepDefinitions.StepOverloaded,
                    Feature = new Feature
                    {
                        StepDefinitions = {typeof(Feature), typeof(StepDefinitions)}
                    }
                }
            )
            .Code.ShouldContain("StepDefinitions.Step(\"42\");");
        }

        [Test]
        public void should_format_args_for_tables_according_the_declared_type_on_implementation()
        {
            ObjectFactory.NewStepRunnerGenerator
            (
                new Step
                {
                    Name = "Given_the_Board",
                    Table = new Table { Rows =
                    {
                        new List<string> {"0","" ,"" }, 
                        new List<string> {"" ,"X","" }, 
                        new List<string> {"" ,"" ,"X"}, 
                    }},
                    Method = Common.StepDefinitions.StepWithTable
                }
            )
            .Code.TrimLines().ShouldContain
            (@"
		        Given_the_Board
		        (
			        new[] {""0"", """", """"},
			        new[] {"""", ""X"", """"},
			        new[] {"""", """", ""X""}
		        );
            ".TrimLines());
        }

        [Test]
        public void should_format_Args_for_Tables_with_Header_according_the_declared_type_on_implementation()
        {
            ObjectFactory.NewStepRunnerGenerator
            (
                new Step
                {
                    Name = "Given_the_Address",
                    Table = new Table 
                    { 
                        HasHeader = true, 
                        Rows =
                        {
                            new List<string> { "state", "zip" }, 
                            new List<string> { "FL" , "33131" }, 
                            new List<string> { "NY" , "10001" }, 
                        }
                    },
                    Method = Common.StepDefinitions.StepWithTwoArgs
                }
            )
            .Code.TrimLines().ShouldContain
            (@"
		        Given_the_Address(""FL"", ""33131"");
		        Given_the_Address(""NY"", ""10001"");
            "
            .TrimLines());
        }

        [Test]
        public void should_format_args_for_tables_with_args_according_the_declared_type_on_implementation()
        {
            ObjectFactory.NewStepRunnerGenerator
            (
                new Step
                {
                    Name = "Given_the_Board",
                    Args = new List<string> { "0" },
                    Table = new Table { Rows =
                    {
                        new List<string> {"0","" ,"" }, 
                        new List<string> {"" ,"X","" }, 
                        new List<string> {"" ,"" ,"X"}, 
                    }},
                    Method = Common.StepDefinitions.StepWithTableAndArg
                }
            )
            .Code.TrimLines().ShouldContain
            (@"
		        Given_the_Board
		        (
                    ""0"",
			        new[] {""0"", """", """"},
			        new[] {"""", ""X"", """"},
			        new[] {"""", """", ""X""}
		        );
            ".TrimLines());
        }

        [Test]
        [Row("default", typeof(object), "default")]
        [Row("string", typeof(string), "\"string\"")]
        [Row("multiline \r\n string", typeof(string), "\r\n@\"multiline \r\n string\"")]
        [Row("1/1/77", typeof(DateTime), "System.DateTime.Parse(\"1/1/77\")")]
        public void should_format_args_according_types(string Arg, Type Type, string ExpectedFormat)
        {
            ArgFormatter.Format(Arg, Type).ShouldBe(ExpectedFormat);
        }
        
        [Test]
        [Row("UserName")]
        [Row("username")]
        [Row("User Name")]
        [Row("user name")]
        public void should_match_Header_to_ObjectArg_field(string UserName)
        {
            Feature = new Feature 
            { 
                Name = "StepDefinitions",
                DeclaredStepDefinitions = new List<string> { "StepDefinitions" },
                Scenarios = { new Scenario { Steps = { new Step
                {
                    Name = "Step_with_object",
                    Table = new Table 
                    { 
                        HasHeader = true, 
                        Rows = { new List<string> { UserName } }
                    }
                }}
            }}};

            ObjectFactory.NewFeatureCompiler.Compile(Feature, FeatureItem);

            Feature.Steps[0].Method
                .ShouldBe(Common.StepDefinitions.StepWithObject);
        }
    }
}