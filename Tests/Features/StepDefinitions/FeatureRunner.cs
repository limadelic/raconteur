using System.Collections;
using Common;
using FluentSpec;
using Raconteur;
using Raconteur.Helpers;
using Raconteur.IDE;

namespace Features.StepDefinitions
{
    public class FeatureRunner
    {
        public string FeatureContent;

        public Feature Feature
        {
            get
            {
                var FeatureFile = new FeatureFile { Content = FeatureContent };

                var FeatureItem = Actors.FeatureItem("Features.dll", "Features");

                var NewFeature = ObjectFactory.NewFeatureParser
                    .FeatureFrom(FeatureFile, FeatureItem);

                ObjectFactory.NewFeatureCompiler.Compile(NewFeature, FeatureItem);

                return NewFeature;
            }            
        }

        // Tech Debt: why not using real code?
        public string Runner
        {
            get
            {
                return ObjectFactory.NewRunnerGenerator(Feature).Code.TrimLines();
            } 
        }

        public void Given_the_Feature_is(string Content)
        {
            FeatureContent = Content;
        }

        public virtual void Given_the_Feature_contains(string Content)
        {
            FeatureContent = 
            @"
                Feature: Feature Name
            "
            + Content;
        }

        public virtual void Given_the_Feature_contains_a(string Content)
        {
            FeatureContent = 
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