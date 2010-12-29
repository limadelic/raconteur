using System.Collections.Generic;
using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Parsers;

namespace Specs
{
    public class When_grouping_tests
    {
        [TestFixture]
        public class the_scenarios : BehaviourOf<ScenarioTokenizerClass>
        {
            [Test]
            public void should_include_tags()
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
        }
    }
}