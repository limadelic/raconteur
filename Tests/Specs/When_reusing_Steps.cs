using System;
using System.Collections.Generic;
using Common;
using FluentSpec;
using MbUnit.Framework;
using NSubstitute;
using Raconteur;
using Raconteur.Generators;
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
            StepLibraries = StepLibraries
        };

        static readonly List<Type> StepLibraries = new List<Type>
        {
            typeof(StepLibrary), typeof(AnotherStepLibrary), typeof(StepLibraryInSameNamespace)
        };

        FeatureItem FeatureItem;

        FeatureParserClass Parser;
        
        #region setup

        dynamic backup;

        [SetUp]
        public void SetUp() 
        {
            SetUpFeatureItem();
            SetUpParser();

            backup = Settings.StepLibraries;
        }

        void SetUpFeatureItem() {
            FeatureItem = Substitute.For<FeatureItem>();
            FeatureItem.Assembly.Returns("Common");
        }

        void SetUpParser() 
        {
            Parser = new FeatureParserClass 
            {
                ScenarioTokenizer = Substitute.For<ScenarioTokenizer>(),
                TypeResolver = Substitute.For<TypeResolver>()
            };

            Parser.TypeResolver.TypeOf("StepLibrary", "Common").Returns(typeof(StepLibrary));
            Parser.TypeResolver.TypeOf("AnotherStepLibrary", "Common").Returns(typeof(AnotherStepLibrary));
            Parser.TypeResolver.TypeOf("StepLibraryInSameNamespace", "Common").Returns(typeof(StepLibraryInSameNamespace));
        }

        [TearDown]
        public void TearDown()
        {
            Settings.StepLibraries = backup;
        }

        #endregion

        [Test]
        public void should_find_the_StepLibraries()
        {
            var Feature = Parser.FeatureFrom(new FeatureFile
            {
                Content = 
                @"
                    Feature: Name

                    using Step Library
                    using Another Step Library
                    using StepLibraryInSameNamespace
                "
            }, FeatureItem);
            
            Feature.StepLibraries.ShouldBe(StepLibraries);
        }

        [Test]
        public void should_include_the_StepsLibraries_from_Settings()
        {
            Settings.StepLibraries = new List<string> 
            {
                "StepLibrary", "AnotherStepLibrary", "StepLibraryInSameNamespace"
            };

            var Feature = Parser.FeatureFrom(new FeatureFile
            {
                Content = 
                @"
                    Feature: Name
                "
            }, FeatureItem);

            Feature.StepLibraries.ShouldBe(StepLibraries);
        }

        [Test]
        public void should_find_types_in_different_assemblies()
        {
            new TypeResolverClass()
                .TypeOf("StepLibrary", "Common.dll")
                .Name.ShouldBe("StepLibrary");
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
        public void should_declare_the_StepLibraries()
        {
            new RunnerGenerator(Feature).Code.TrimLines().ShouldContain
            (@"
                public StepLibrary StepLibrary = new StepLibrary();
                public AnotherStepLibrary AnotherStepLibrary = new AnotherStepLibrary();
            "
            .TrimLines());
        }

        [Test]
        public void should_use_Steps_from_libraries()
        {
            new StepGenerator
            (
                new Step { Name = "Step" }, 
                StepLibraries
            )
            .Code.ShouldContain("StepLibrary.Step();");

            new StepGenerator
            (
                new Step { Name = "another_Step" }, 
                StepLibraries
            )
            .Code.ShouldContain("AnotherStepLibrary.another_Step();");
        }
    }
}