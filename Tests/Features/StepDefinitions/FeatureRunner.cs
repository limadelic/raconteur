using System.Collections;
using Common;
using FluentSpec;
using NSubstitute;
using Raconteur;
using Raconteur.Generators;
using Raconteur.IDE;

namespace Features.StepDefinitions
{
    public class FeatureRunner
    {
        public string Feature;

        public string Runner
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

        public void Given_the_Feature_is(string Content)
        {
            Feature = Content;
        }

        public virtual void Given_the_Feature_contains(string Content)
        {
            Feature = 
            @"
                Feature: Feature Name
            "
            + Content;
        }

        public void The_Runner_should_be(string Content)
        {
            Runner.ShouldBe(Content.TrimLines());
        }

        public void The_Runner_should_contain(string Content)
        {
            Runner.ShouldContain(Content.TrimLines());
        }

        public void Given_the_setting__contains(string Setting, string Value)
        {
            ((IList)Settings.Setting.Get(Setting)).Add(Value);
        }

        public void And_the_Steps_are(string StepsClass) { }
    }
}