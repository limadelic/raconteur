using System.Collections.Generic;
using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Generators;
using Raconteur.Parsers;

namespace Specs
{
    public class When_testing_with_outlines
    {
        [TestFixture]
        public class while_parsing_Scenarios : BehaviourOf<ScenarioParserClass>
        {
            Scenario Scenario;

            [SetUp]
            public new void SetUp()
            {
                Scenario = The.ScenarioFrom(new List<string>
                {
                    "Scenario Outline: Add",
                    @"The sum of ""A"" + ""B"" should be ""C""",
                    "Examples:",
                    "|A|B|C|",
                    "|0|0|0|",
                    "|0|1|1|"
                });
            }

            [Test]
            public void should_add_the_Examples_as_a_Scenario_Table()
            {
                Scenario.Examples.Rows.Count.ShouldBe(3);
            }

            [Test]
            public void should_add_the_Outline_as_Steps()
            {
                Scenario.Steps.Count.ShouldBe(1);
            }
        }

        [TestFixture]
        public class a_generator : BehaviourOf<RunnerGenerator>
        {
            Feature Feature;
            Scenario Scenario;

            [SetUp]
            public new void SetUp()
            {
                Feature = Actors.Feature;
                Scenario = Feature.Scenarios[0];
                
                Scenario.Examples = new Table
                {
                    Rows = new List<List<string>>
                    {
                        new List<string> {"X", "Y"},
                        new List<string> {"1", "2"},
                        new List<string> {"3", "4"}
                    }
                };

//                Scenario.Steps[0].Args = new List<string> {"X"};
//                Scenario.Steps[1].Args = new List<string> {"Y"};
            }

            [Test]
            public void should_create_a_Test_per_Example()
            {
                The.RunnerFor(Feature).ShouldContainInOrder
                (
                    "public void Scenario11", 
                    "public void Scenario12"
                );
            }
        }

    }
}