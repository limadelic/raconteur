using FluentSpec;
using MbUnit.Framework;
using Raconteur.Parsers;

namespace Specs
{
    public class When_testing_with_outlines
    {
        [TestFixture]
        public class The_parser : BehaviourOf<StepParserClass>
        {
            [Test]
            public void should_keep_track_of_every_Step()
            {
                Given.StepFrom("Step 1");
                And.StepFrom("Step 2");
                Then.Steps.Count.ShouldBe(2);
            }

            [Test]
            public void should_skip_already_generated_Steps_if_encounters_an_Outline_Arg()
            {
                Given.StepFrom("Step 1");
                When.StepFrom("Step <2>");
                Then.Steps.ForEach(Step => Step.Skip.ShouldBeTrue());
            }

            [Test]
            public void the_Outline_Arg_should_become_a_regular_Arg()
            {
                The.StepFrom("Step <2>").Args[0].ShouldBe("2");
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
    }
}