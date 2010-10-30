using FluentSpec;
using Raconteur;
using Raconteur.Generators;
using Raconteur.IDE;

namespace Features
{
    public class FeatureRunner
    {
        protected string Feature;

        protected string Runner
        {
            get
            {
                var FeatureFile = new FeatureFile { Content = Feature };

                var Parser = ObjectFactory.NewFeatureParser;

                var Project = new VsFeatureItem { DefaultNamespace = "Features" };

                var NewFeature = Parser.FeatureFrom(FeatureFile, Project);

                var Code = new RunnerGenerator(NewFeature).Code;

                return Code.TrimLines();
            } 
        }

        protected void Given_the_Feature_is(string Feature)
        {
            this.Feature = Feature;
        }

        protected void Given_the_Feature_contains(string Feature)
        {
            this.Feature = 
            @"
                Feature: Feature Name
            "
            + Feature;
        }

        protected void The_Runner_should_be(string Runner)
        {
            this.Runner.ShouldBe(Runner.TrimLines());
        }

        protected void The_Runner_should_contain(string Runner)
        {
            this.Runner.ShouldContain(Runner.TrimLines());
        }
    }
}