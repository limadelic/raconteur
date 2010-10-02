using System.Collections.Generic;
using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Parsers;

namespace Specs
{
    public class When_testing_with_outlines
    {
/*
        [TestFixture]
        public class while_parsing_Scenarios : BehaviorOf<ScenarioParserClass>
        {
            List<Scenario> Scenarios;

            [SetUp]
            public void SetUp()
            {
                Scenarios = The.ScenariosFrom
                (@"
                    Scenario Outline: Add
                      The sum of <A> + <B> should be <C>
                      Examples:
                        |A|B|C|
                        |0|0|0|
                        |0|1|1|
                ");
            }

            [Test]
            public void should_get_a_Scenario_per_Example()
            {
                Scenarios.Count.ShouldBe(2);
            }

            [Test]
            public void should_use_the_name_in_all_Scenarios_plus_index()
            {
                Scenarios[0].Name.ShouldBe("Add1");
                Scenarios[1].Name.ShouldBe("Add2");
            }

            [Test]
            public void should_substitute_Example_in_Outline()
            {
                Scenarios[0].Steps[0]
                    .ShouldBe("The_sum_of____should_be_", 0, 0, 0);

                Scenarios[1].Steps[0]
                    .ShouldBe("The_sum_of____should_be_", 0, 1, 1);
            }
        }

        [TestFixture]
        public class The_parser : BehaviourOf<StepParserClass>
        {
            [Test]
            public void the_Outline_Arg_should_become_a_regular_Arg()
            {
                The.OutlineStepFrom("Step <2>").Args[0].ShouldBe("2");
            }

            [Test]
            public void should_group_Args_names_according_to_table_headers()
            {
                var Arg1 = Given.StepFrom("Step <Arg>").Args[0];
                When.StepFrom("|Arg|");
                Then.ArgColMap[0][0].ShouldBe(Arg1);
            }

            [Test]
            public void should_return_the_Step_Outlines_with_Args_from_each_row()
            {
                
            }
        }
*/
    }
}