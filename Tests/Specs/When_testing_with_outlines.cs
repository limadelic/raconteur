using System.Collections.Generic;
using System.Linq;
using Common;
using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Helpers;
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
                    "Examples: Name",
                    "|A|B|C|",
                    "|0|0|0|",
                    "|0|1|1|"
                });
            }

            [Test]
            public void should_get_the_name_of_Examples()
            {
                Scenario.Examples[0].Name.ShouldBe("Name");
            }

            [Test]
            public void should_add_the_Examples_as_a_Scenario_Table()
            {
                Scenario.Examples[0].Count.ShouldBe(2);
            }

            [Test]
            public void should_add_the_Outline_as_Steps()
            {
                Scenario.Steps.Count.ShouldBe(1);
            }
        }

        [TestFixture]
        public class while_parsing_multiple_Examples : BehaviourOf<ScenarioParserClass>
        {
            Scenario Scenario;

            [SetUp]
            public new void SetUp()
            {
                Scenario = The.ScenarioFrom(new List<string>
                {
                    "Scenario Outline: Add",
                    @"The sum of ""A"" + ""B"" should be ""C""",
                    "Examples: Positives",
                    "|A|B|C|",
                    "|0|0|0|",
                    "|0|1|1|",
                    "Examples: Negatives",
                    "|A |B |C |",
                    "|0 |-1|-1|",
                    "|-1|-1|-2|"
                });
            }

            [Test]
            public void should_get_the_name_of_Examples()
            {
                Scenario.Examples[0].Name.ShouldBe("Positives");
                Scenario.Examples[1].Name.ShouldBe("Negatives");
            }

            [Test]
            public void should_add_the_Examples_as_a_Scenario_Table()
            {
                Scenario.Examples[0].Count.ShouldBe(2);
                Scenario.Examples[1].Count.ShouldBe(2);
            }
        }

        [TestFixture]
        public class a_generator
        {
            string Runner;

            [SetUp]
            public void SetUp()
            {
                var Feature = Actors.Feature;
                var Scenario = Feature.Scenarios[0];
                
                Scenario.Examples = new List<Table> 
                {
                    new Table
                    {
                        Rows = new List<List<string>>
                        {
                            new List<string> {"X", "Y"},
                            new List<string> {"1", "2"},
                            new List<string> {"3", "4"}
                        }
                    },
                    new Table
                    {
                        Name = "Name",
                        Rows = new List<List<string>>
                        {
                            new List<string> {"X", "Y"},
                            new List<string> {"5", "6"},
                            new List<string> {"7", "8"}
                        }
                    },
                };

                Scenario.Steps[0].Args = new List<string> {"X"};
                Scenario.Steps[1].Args = new List<string> {"Y"};

                Runner = ObjectFactory.NewRunnerGenerator(Feature).Code;            
            }

            [Test]
            public void should_create_a_Test_per_Example()
            {
                Runner.ShouldContainInOrder
                (
                    "public void Scenario1_1()", 
                    "public void Scenario1_2()",
                    "public void Scenario1_Name1()", 
                    "public void Scenario1_Name2()"
                );
            }

            [Test]
            public void should_pass_Examples_as_Args()
            {
                Runner.ShouldContainInOrder
                (
                    "public void Scenario1_1()", 
                        "Unique_step(1)", 
                        "Repeated_step(2)",
                    "public void Scenario1_2()",
                        "Unique_step(3)", 
                        "Repeated_step(4)",
                    "public void Scenario1_Name1()",
                        "Unique_step(5)", 
                        "Repeated_step(6)",
                    "public void Scenario1_Name2()",
                        "Unique_step(7)", 
                        "Repeated_step(8)"
                );
            }
        }

    }
}