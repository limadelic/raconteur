using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Parsers;

namespace Specs
{
    public class When_testing_with_tables
    {
        [TestFixture]
        public class The_parser : BehaviourOf<StepParserClass>
        {
            [SetUp]
            public new void SetUp()
            {
                Given.LastStep = new Step();
            }

            [Test]
            public void should_keep_last_Step_and_skip_it_from_output()
            {
                Given.StepFrom("Verify some values");
                When.StepFrom("|X|Y|");
                Then.LastStep.Name.ShouldBe("Verify_some_values");
                 And.LastStep.Skip.ShouldBeTrue();
            }

            [Test]
            public void should_should_skip_the_first_row()
            {
                The.StepFrom("|X|Y|").ShouldBeNull();
            }

            [Test]
            public void should_name_every_row_as_last_Step()
            {
                Given.LastStep.Name = "Verify_some_values";
                 And.StepFrom("|X|Y|");
                
                Then.StepFrom("|1|0|").Name.ShouldBe("Verify_some_values");
                 And.StepFrom("|0|1|").Name.ShouldBe("Verify_some_values");
            }

            [Test]
            public void should_define_the_cols_as_Args()
            {
                Given.LastStep.Name = "Verify_some_values";
                 And.StepFrom("|X|Y|");

                var Args = The.StepFrom("|0|1|").Args;
                
                Args.Count.ShouldBe(2);
                Args.ShouldBe("0", "1");
            }
        }
    }
}