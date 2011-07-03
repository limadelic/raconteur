using System.Collections.Generic;
using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Helpers;
using Raconteur.Parsers;

namespace Specs
{
    [TestFixture]
    public class KeepFeatureLocation
    {
        [Test]

        [Row("One Scenario",
            @"
                Scenario: One
            ",
            0, "Scenario: One")]

        [Row("Many single line Scenarios",
            @"
                Scenario: One
                Scenario: Two
                Scenario: Three
            ",
            1, "Scenario: Two")]

        [Row("Scenarios with tags",
            @"
                Scenario: One
                Scenario: Two
                Scenario: Three
            ",
            1, "Scenario: Two")]

        [Row("Scenarios with tags",
            @"
                Scenario: One
                    Step

                @tag
                Scenario: Another
            ",
            1, "@tag")]

        public void Tokenizer_should_include_Scenario_location
        (
            string Example, 
            string Content, 
            int ExpectedIndex, 
            string ExpectedContent
        )
        {
            new ScenarioTokenizerClass
            {
                Content =
                @"
                    Feature: Name
                "
                + Content
            }
            .ScenarioDefinitions[ExpectedIndex].Item2
                .Content.ShouldStartWith(ExpectedContent);
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

        [Row("Simple Step",
            @"
                Step 1
                Step 2
            ",
            "Step 2")]

        [Row("Step with Args",
            @"
                ""Arg"" Step ""Arg"" 
            ",
            @"""Arg"" Step ""Arg""")]

        [Row("Step with Multiline Args",
            @"
                A multiline step arg
                ""
                    line 1
                    line 2
                ""
            ",
            "A multiline step arg")]

        [Row("Step with all kind of Args",
            @"
                ""
                    line 1
                    line 2
                ""
                A multiline ""Arg"" step arg
                ""
                    line 1
                    line 2
                ""
            ",
            @"A multiline ""Arg"" step arg")]

        public void StepParser_should_include_Step_location
        (
            string Example, 
            string Content, 
            string LocationContent
        )
        {
            var Parser = new StepParserClass();

            var Location = new Location { Content = "Scenario: Name" + "Content" };

            Step Step = null;
            
            Content.TrimLines().Lines().ForEach(l => 
                Step = Parser.StepFrom(l, Location) ?? Step);

            Step.Location.Content.ShouldBe(LocationContent);
        }
    }
}