using System.Collections.Generic;
using Common;
using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Helpers;
using Raconteur.IDEIntegration.Intellisense;

namespace Specs
{
    [TestFixture]
    public class When_calculating_Completions : BehaviorOf<CompletionCalculator>
    {
        private Language current;

        [SetUp]
        public void Setup()
        {
            BackupCurrentLanguage();
            Given.Feature.Is(new Feature { Name = "FeatureName" } );
        }

        [Test]
        public void should_complete_keywords()
        {
            Given.Compiler.StepNamesOf(null, null)
                .IgnoringArgs().WillReturn(new List<string>());
            
            When.For("Sc").ShouldContain("Scenario:");
            And.For("Sc").ShouldContain("Scenario Outline:");
            And.For("Fe").ShouldContain("Feature:");
            And.For("Ex").ShouldContain("Examples:");
            And.For("Scenario O").ShouldNotContain("Scenario:");
        }

        [Test]
        public void should_complete_repeated_Steps()
        {
            const string FirstStep = "I'm knocking on the doors of your Hummer";
            const string SecondStep = "Yeah, we hungry like the wolves hunting dinner";
            
            Given.Feature.Steps.Is( 
                new List<Step> { 
                    new Step{ Name = FirstStep },
                    new Step{ Name = SecondStep}
            });

            When.For("I'm").ShouldContain(FirstStep);
            When.For("Ye").ShouldContain(SecondStep);
        }

        [Test]
        public void should_remove_Arg_values()
        {
            Given.Feature.Steps.Is(
                new List<Step> {
                    new Step{Name = @"Not a ""second"" time"}     
            }); 

            When.For("Not").ShouldContain("Not a \"\" time");
            When.For("Not").ShouldNotContain("Not a \"second\" time");
        }

        [Test]
        public void should_respect_language()
        {
            Settings.Language = Languages.In["hr"];

            When.For("Os").ShouldContain("Osobina:");
        }

        [TearDown]
        public void TearDown()
        {
            ResetLanguage();
        }

        private void ResetLanguage()
        {
            Settings.Language = current;
        }

        private void BackupCurrentLanguage()
        {
            current = Settings.Language;
        }
    }
}