using System;
using System.Collections.Generic;
using FluentSpec;
using MbUnit.Framework;
using Raconteur.Parsers;
using Common;

namespace Specs
{
    [TestFixture]
    public class KeepFeaturePosition
    {
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

            Scenarios[1].Item2.ShouldBe(126, 187);
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
    }
}