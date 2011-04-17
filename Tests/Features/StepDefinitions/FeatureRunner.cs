using System.Collections;
using Common;
using FluentSpec;
using NSubstitute;
using Raconteur.Helpers;
using Raconteur.IDE;

namespace Features.StepDefinitions
{
    public class FeatureRunner
    {
        public string Feature;

        // Tech Debt: why not using real code?
        public string Runner
        {
            get
            {
                var FeatureFile = new FeatureFile { Content = Feature };

                var FeatureItem = Substitute.For<FeatureItem>();
                FeatureItem.DefaultNamespace = "Features";
                FeatureItem.Assembly.Returns("Features.dll");

                var NewFeature = ObjectFactory.NewFeatureParser
                    .FeatureFrom(FeatureFile, FeatureItem);

                ObjectFactory.NewFeatureCompiler.Compile(NewFeature, FeatureItem);

                var Code = ObjectFactory.NewRunnerGenerator(NewFeature).Code;

                return Code.TrimLines();
            } 
        }

        public void Given_the_Feature_is(string Content)
        {
            Feature = Content;
        }

        public void Given_the_Feature_is_the(string Content)
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
    }
}