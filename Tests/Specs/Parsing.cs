using System.Collections.Generic;
using FluentSpec;
using MbUnit.Framework;
using Raconteur.Parsers;
using Common;

namespace Specs
{
    [TestFixture]
    public class KeepFeatureLocation
    {
        [Test]
        public void Tokenizer_should_include_Scenario_location_with_one_scenario()
        {
            var Scenarios = new ScenarioTokenizerClass
            {
                Content =
                @"
                    Feature: Name

                    Scenario: One
                "
            }.ScenarioDefinitions;

            Scenarios[0].Item2.ShouldBe(59, 90);
            Scenarios[0].Item2.Content.ShouldStartWith("Scenario: One");
        }

        [Test]
        public void Tokenizer_should_include_Scenario_location()
        {
            var Scenarios = new ScenarioTokenizerClass
            {
                Content =
                @"
                    Feature: Name

                    Scenario: One
                        Step

                    @tag
                    Scenario: Another
                "
            }.ScenarioDefinitions;

            Scenarios[0].Item2.ShouldBe(59, 125);
            Scenarios[0].Item2.Content.ShouldStartWith("Scenario: One");

            Scenarios[1].Item2.ShouldBe(126, 187);
            Scenarios[1].Item2.Content.ShouldContain("Scenario: Another");
        }

        [Test]
        public void ScenarioParser_should_include_location_in_Scenario()
        {
            var expectedLocation = new Location();

            new ScenarioParserClass()
                .ScenarioFrom
                (
                    new List<string> { "Scenario: Name" }, 
                    expectedLocation
                )
            .Location.ShouldBe(expectedLocation);
        }

        [Test]
        public void StepParser_should_include_Step_location()
        {
            var Parser = new StepParserClass();

            var Location = new Location
            {
                Start = 10,
                Content = "StepStep"
            };

            Parser.StepFrom(@"Step", Location).Location.ShouldBe(10, 14);
            Parser.StepFrom(@"Step", Location).Location.ShouldBe(14, 18);
        }
    }
}