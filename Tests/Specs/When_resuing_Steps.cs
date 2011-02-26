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
    public class When_resuing_Steps
    {
        readonly Feature Feature = new Feature
        {
            StepLibraries = StepLibraries
        };

        static readonly List<Type> StepLibraries = new List<Type>
        {
            typeof(StepLibrary), typeof(AnotherStepLibrary)
        };

        [Test]
        public void should_find_the_StepLibraries()
        {
            var FeatureItem = Substitute.For<FeatureItem>();

            var Parser = new FeatureParserClass
            {
                ScenarioTokenizer = Substitute.For<ScenarioTokenizer>(),
                TypeResolver = Substitute.For<TypeResolver>()
            };

            FeatureItem.Assembly.Returns("Common");
            Parser.TypeResolver.TypeOf("StepLibrary", "Common").Returns(typeof(StepLibrary));
            Parser.TypeResolver.TypeOf("AnotherStepLibrary", "Common").Returns(typeof(AnotherStepLibrary));

            var Feature = Parser.FeatureFrom(new FeatureFile
            {
                Content = 
                @"
                    Feature: Name

                    using Step Library
                    using Another Step Library
                "
            }, FeatureItem);
            
            Feature.StepLibraries.ShouldBe(StepLibraries);
        }

        [Test]
        public void should_find_types_in_different_assemblies()
        {
            new TypeResolverClass()
            .TypeOf("StepLibrary", "Common").ShouldBe(typeof(StepLibrary));
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