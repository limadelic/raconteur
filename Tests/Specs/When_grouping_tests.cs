using System.Collections.Generic;
using FluentSpec;
using MbUnit.Framework;
using Raconteur.Generators;
using Raconteur.Parsers;

namespace Specs
{
    public class When_grouping_tests
    {
        [TestFixture]
        public class the_scenarios_tokenizer : BehaviourOf<ScenarioTokenizerClass>
        {
            [Test]
            public void should_include_tags_in_definitions()
            {
                Given.Content =
                @"
                    Feature: Tags

                    @tag
                    Scenario: With Tag
                ";

                The.ScenarioDefinitions[0]
                    .Count.ShouldBe(2);
            }

            [Test]
            public void should_include_every_line_after_the_first_tag()
            {
                Given.Content =
                @"
                    Feature: Tags

                    @tag
                    line
                    line
                    Scenario: With Tag
                ";

                The.ScenarioDefinitions[0]
                    .Count.ShouldBe(4);
            }
        }

        [TestFixture]
        public class the_scenarios_parser : BehaviourOf<ScenarioParserClass>
        {
            [Test]
            public void should_read_tags_of_Scenarios()
            {
                The.ScenarioFrom(new List<string>
                {
                    "@a_tag dud @another_tag",
                    "@a_tag dud",
                    "Scenario: With Tag"
                })
                .Tags.ShouldBe("a_tag", "another_tag");
            }
        }

/*
        [TestFixture]
        public class the_Scenario_generator : BehaviourOf<ScenarioGenerator>
        {
            [Test]
        }
*/
    }
}