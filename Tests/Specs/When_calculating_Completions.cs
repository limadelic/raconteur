using MbUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raconteur;
using Raconteur.IDEIntegration.Intellisense;

namespace Specs
{
    [TestFixture]
    public class When_calculating_Completions : BehaviourOf<CompletionCalculator>
    {
        private Language current;

        [TestMethod]
        public void should_complete_keywords()
        {
            When.For("Sc").ShouldContain("Scenario");
            When.For("Sc").ShouldContain("Scenario Outline");
            When.For("Fe").ShouldContain("Feature");
            When.For("Ex").ShouldContain("Examples");
            When.For("Scenario O").ShouldNotContain("Scenario");
        }

        [TestMethod]
        public void should_complete_repeated_Steps()
        {
            Given.Feature = 
                @"I'm knocking on the doors of your Hummer
                Yeah, we hungry like the wolves hunting dinner";

            When.For("I'm").ShouldContain("I'm knocking on the doors of your Hummer");
            When.For("Ye").ShouldContain("Yeah, we hungry like the wolves hunting dinner");
        }

        [TestMethod]
        public void should_respect_language()
        {
            BackupCurrentLanguage();
            Settings.Language = Languages.In["hr"];

            When.For("Os").ShouldContain("Osobina");

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