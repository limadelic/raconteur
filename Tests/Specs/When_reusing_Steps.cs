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
            StepDefinitions = StepDefinitions
        };

        static readonly List<Type> StepDefinitions = new List<Type>
        {
            typeof(StepDefinitions), typeof(AnotherStepDefinitions), typeof(StepDefinitionsInSameNamespace)
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

            backup = Settings.StepDefinitions;
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

            Parser.TypeResolver.TypeOf("StepDefinitions", "Common").Returns(typeof(StepDefinitions));
            Parser.TypeResolver.TypeOf("AnotherStepDefinitions", "Common").Returns(typeof(AnotherStepDefinitions));
            Parser.TypeResolver.TypeOf("StepDefinitionsInSameNamespace", "Common").Returns(typeof(StepDefinitionsInSameNamespace));
        }

        [TearDown]
        public void TearDown()
        {
            Settings.StepDefinitions = backup;
        }

        #endregion

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
            
            Feature.StepDefinitions.ShouldBe(StepDefinitions);
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

            Feature.StepDefinitions.ShouldBe(StepDefinitions);
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
                new Step { Name = "Step" }, 
                StepDefinitions
            )
            .Code.ShouldContain("StepDefinitions.Step();");

            new StepGenerator
            (
                new Step { Name = "another_Step" }, 
                StepDefinitions
            )
            .Code.ShouldContain("AnotherStepDefinitions.another_Step();");
        }
    }
}