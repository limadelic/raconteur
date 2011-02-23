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


namespace Specs
{
    [TestFixture]
    public class When_resuing_Steps
    {
        [Test]
        public void should_find_the_StepLibrary()
        {
            var Parser = new FeatureParserClass
            {
                ScenarioTokenizer = Substitute.For<ScenarioTokenizer>(),
                TypeResolver = Substitute.For<TypeResolver>()
            };

            Parser.TypeResolver.TypeOf("StepLibrary").Returns(typeof(StepLibrary));

            var Feature = Parser.FeatureFrom(new FeatureFile
            {
                Content = 
                @"
                    Feature: Name

                    using Step Library
                "
            }, new VsFeatureItem());
            
            Feature.StepLibrary.ShouldBe(typeof(StepLibrary));

/*
    Given.Sut.IsA<FeatureParserClass>();
    And.TypeOf("StepLibrary").Is(typeof(StepLibrary))
    When.Parse(...)
    The.StepLibrary.ShouldBe(typeof(StepLibrary));
*/
        }

        [Test]
        public void should_find_types_in_different_assemblies()
        {
            new TypeResolverClass
            {
                Assembly = "Common"
            }
            .TypeOf("StepLibrary").ShouldBe(typeof(StepLibrary));
        }

        readonly Feature Feature = new Feature
        {
            StepLibrary = typeof(StepLibrary)
        };

        [Test]
        public void should_add_namespace_to_runner()
        {
            new RunnerGenerator(Feature).Code.ShouldContain("using Common;");
        }

        [Test]
        public void should_declare_StepLibrary()
        {
            new RunnerGenerator(Feature).Code
                .ShouldContain("public StepLibrary StepLibrary = new StepLibrary();");
        }

        [Test]
        public void should_use_Step_from_library()
        {
            new StepGenerator
            (
                new Step { Name = "Step" }, 
                typeof(StepLibrary)
            )
            .Code.ShouldContain("StepLibrary.Step();");
        }
    }
}