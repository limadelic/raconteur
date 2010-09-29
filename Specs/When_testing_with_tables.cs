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
            [Test]
            public void should_should_skip_the_first_row()
            {
                The.StepFrom("|X|Y|").ShouldBeNull();
            }

            [Test]
            public void should_should_name_every_row_as_last_Step()
            {
                Given.LastStep = new Step {Name = "Verify_some_values"};
                 And.StepFrom("|X|Y|");
                
                Then.StepFrom("|1|0|").Name.ShouldBe("Verify_some_values");
                 And.StepFrom("|1|0|").Name.ShouldBe("Verify_some_values");
            }
        }
    }
}