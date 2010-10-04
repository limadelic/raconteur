using System.Collections.Generic;
using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Generators;
using Raconteur.Parsers;

namespace Specs
{
    public class When_testing_with_tables
    {
        [TestFixture]
        public class The_parser : BehaviourOf<StepParserClass>
        {
            [Test] 
            public void should_associate_table_and_step()
            {
                var Step = 
                When.StepFrom("Step table:");
                And.StepFrom("|X|Y|");
                And.StepFrom("|2|1|");
                
                Step.Table.Rows.Count.ShouldBe(2);
            }
        }

        [TestFixture]
        public class The_generator : BehaviourOf<RunnerGenerator>
        {
            Feature Feature;
            Step Step;
            string Runner;
            
            [SetUp]
            public void SetUp() 
            {
                Feature = Actors.Feature;
                Step = Feature.Scenarios[0].Steps[0];
                
                Step.Table = new Table
                {
                    Rows = new List<List<string>>
                    {
                        new List<string> {"X", "Y"},
                        new List<string> {"1", "2"},
                        new List<string> {"3", "4"}
                    }
                };

                Runner = The.RunnerFor(Feature);            
            }

            [Test]
            public void should_should_skip_the_first_row()
            {
                Runner.ShouldNotContain(Step.Name + @"(""X"", ""Y"");");
            }

            [Test]
            public void should_create_a_step_for_every_row()
            {
                Runner.ShouldContain(Step.Name + @"(1, 2);");
                Runner.ShouldContain(Step.Name + @"(3, 4);");
            }

            /*
            [Test]
            public void should_define_the_cols_as_Args()
            {
                Given.LastStep.Name = "Verify_some_values";
                 And.StepFrom("|X|Y|");

                The.StepFrom("|0|1|")
                    .Args.ShouldBe("0", "1");
            }

            [Test]
            public void should_combine_Table_Args_with_col_as_Args()
            {
                Given.LastStep.Name = "Given_stuff_in__place";
                 And.LastStep.Args.Add("\"X\"");
                 And.StepFrom("| stuff |");

                The.StepFrom("| one |")
                    .Args.ShouldBe("\"X\"", "\"one\"");
            }
*/
        }
    }
}