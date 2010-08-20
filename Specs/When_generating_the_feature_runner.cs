using System;
using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Generators;
using Raconteur.Parsers;

namespace Specs
{
    [TestFixture]
    public class When_generating_the_feature_runner
    {
        [TestFixture]
        public class A_runner_generator : BehaviorOf<RunnerGenerator>
        {
            [Test]
            public void should_generate_the_step_definition_reference() 
            {
                var FeatureFile = new FeatureFile { Content = "FeatureFileContent" };
                var Feature = new Feature { Name = "FeatureName" };

                Given.Parser.FeatureFrom("FeatureFileContent").Is(Feature);

                The.RunnerFor(FeatureFile).ShouldContain("StepDefinitions" +
                ".FeatureName Steps = new StepDefinitions.FeatureName();");
            }
        }

        [TestFixture]
        public class A_feature_parser : BehaviorOf<FeatureParserClass>
        {
            [Test]
            public void should_read_the_name()
            {
                The.FeatureFrom("Feature: Feature Name" + Environment.NewLine + "whatever").Name
                    .ShouldBe("FeatureName");

                The.FeatureFrom("Feature: Feature Name").Name
                    .ShouldBe("FeatureName");
            }

            [Test]
            public void should_cammel_case_the_name()
            {
                The.FeatureFrom("Feature: feature name").Name
                    .ShouldBe("FeatureName");
            }
        }
    }
}