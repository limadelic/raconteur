using FluentSpec;
using NSubstitute;
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

                var FeatureItem = Substitute.For<FeatureItem>();
                FeatureItem.DefaultNamespace = "Features";
                FeatureItem.Assembly.Returns("Features.dll");

                var NewFeature = Parser.FeatureFrom(FeatureFile, FeatureItem);

                var Code = ObjectFactory.NewRunnerGenerator(NewFeature).Code;

                return Code.TrimLines();
            } 
        }

        protected void Given_the_Feature_is(string Feature)
        {
            this.Feature = Feature;
        }

        protected virtual void Given_the_Feature_contains(string Feature)
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