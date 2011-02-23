using System.Collections.Generic;
using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Generators;
using Raconteur.Parsers;
using ShouldAssertions = Common.ShouldAssertions;

namespace Specs
{
    [TestFixture]
    public class When_tagging_scenarios
    {
        [Test]
        public void should_include_tags_in_definitions()
        {
            var Sut = new ScenarioTokenizerClass
            {
                Content =
                @"
                    Feature: Tags

                    @tag
                    Scenario: With Tag
                "
            };

            Sut.ScenarioDefinitions[0]
                .Count.ShouldBe(2);
        }

        [Test]
        public void should_include_every_line_after_the_first_tag()
        {
            var Sut = new ScenarioTokenizerClass
            {
                Content =
                @"
                    Feature: Tags

                    @tag
                    line
                    line
                    Scenario: With Tag
                "
            };

            Sut.ScenarioDefinitions[0]
                .Count.ShouldBe(4);
        }

        [Test]
        public void should_read_tags_of_Scenarios()
        {
            var Sut = new ScenarioParserClass();

            ShouldAssertions.ShouldBe(Sut.ScenarioFrom(new List<string>
                {
                    "@a tag @another tag",
                    "@a tag",
                    "Scenario: With Tag"
                })
                .Tags, "a tag", "another tag");
        }

        readonly Scenario Scenario = new Scenario { Tags = { "tag" } };

        [Test]
        public void should_add_Scenario_Tags_to_tests()
        {
            var Sut = new ScenarioGenerator(Scenario);

            Sut.Code.ShouldContain(@"[TestCategory(""tag"")]");
        }

        [Test]
        public void should_use_propper_xUnit_attribute()
        {
            var backup = Settings.XUnit;
            Settings.XUnit = XUnits.Framework["mbunit"];

            var Sut = new ScenarioGenerator(Scenario);

            try { Sut.Code.ShouldContain(@"[Category(""tag"")]"); }
            finally { Settings.XUnit = backup; }
        }
    }
}