using Common;
using FluentSpec;
using MbUnit.Framework;
using Raconteur.Helpers;
using Raconteur.IDEIntegration.Intellisense;

namespace Specs
{
    [TestFixture]
    public class When_calculating_Completions : BehaviorOf<CompletionCalculator>
    {
        private Language current;
        private const string scenarioDeclaration = "Scenario: Test Scenario\r\n";

        [Test]
        public void should_complete_keywords()
        {
            Given.Feature = scenarioDeclaration;
            When.For("Sc").ShouldContain("Scenario:");
            When.For("Sc").ShouldContain("Scenario Outline:");
            When.For("Fe").ShouldContain("Feature:");
            When.For("Ex").ShouldContain("Examples:");
            When.For("Scenario O").ShouldNotContain("Scenario:");
        }

        [Test]
        public void should_complete_repeated_Steps()
        {
            Given.Feature = scenarioDeclaration + 
                @"I'm knocking on the doors of your Hummer
                Yeah, we hungry like the wolves hunting dinner";

            When.For("I'm").ShouldContain("I'm knocking on the doors of your Hummer");
            When.For("Ye").ShouldContain("Yeah, we hungry like the wolves hunting dinner");
        }

        [Test]
        public void should_remove_Arg_values()
        {
            Given.Feature = scenarioDeclaration + @"Not a ""second"" time";

            When.For("Not").ShouldContain("Not a \"\" time");
            When.For("Not").ShouldNotContain("Not a \"second\" time");
        }

        [Test]
        public void should_respect_language()
        {
            BackupCurrentLanguage();
            Settings.Language = Languages.In["hr"];


            Given.Feature = scenarioDeclaration;
            When.For("Os").ShouldContain("Osobina:");

            ResetLanguage();
        }

        [Test]
        public void should_load_names_from_Type()
        {
            Given.Feature = "Feature: When_calculating_Completions";

            When.For("Se").ShouldContain("Setup");
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