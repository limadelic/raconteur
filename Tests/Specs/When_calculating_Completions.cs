using System.Collections.Generic;
using Common;
using FluentSpec;
using MbUnit.Framework;
using NSubstitute;
using Raconteur.Compilers;
using Raconteur.Helpers;
using Raconteur.IDEIntegration.Intellisense;

namespace Specs
{
    [TestFixture]
    public class When_calculating_Completions : BehaviorOf<CompletionCalculator>
    {
        private Language current;
        private FeatureCompiler Compiler;

        [SetUp]
        public void SetUp()
        {
            BackupCurrentLanguage();
            Compiler = Substitute.For<FeatureCompiler>();
            Compiler.StepNamesOf(null, null).ReturnsForAnyArgs(
                new List<string>());

            Given.Feature.Name = "FeatureName";
            Given.Compiler.Is(Compiler);
        }

        [Test]
        public void should_show_all()
        {
            When.For("").ShouldContain("Scenario:");
        }

        [Test]
        public void should_complete_keywords()
        {            
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
            
            Compiler.StepNamesOf(null, null).ReturnsForAnyArgs( 
                new List<string> { 
                    FirstStep,
                    SecondStep
            });

            When.For("I'm").ShouldContain(FirstStep);
            When.For("Ye").ShouldContain(SecondStep);
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