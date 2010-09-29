using FluentSpec;
using MbUnit.Framework;
using Raconteur.Generators;
using Raconteur.Parsers;

namespace Specs 
{
    public class When_testing_with_arguments
    {
        [TestFixture]
        public class The_parser : BehaviorOf<StepParserClass>
        {
            [Test]
            public void should_create_a_Step_with_Args()
            {
                const string Sentence = @"If ""X"" happens";

                The.StepFrom(Sentence)
                    .Name.ShouldBe("If__happens");

                The.StepFrom(Sentence)
                    .Args.Count.ShouldBe(1);
                
                The.StepFrom(Sentence)
                    .Args[0].ShouldBe("\"X\"");
            }

            [Test]
            public void should_create_a_Step_with_multiple_Args()
            {
                const string Sentence = @"If ""X"" and ""Y"" happens";

                The.StepFrom(Sentence)
                    .Name.ShouldBe("If__and__happens");

                The.StepFrom(Sentence).Args.Count.ShouldBe(2);
                
                The.StepFrom(Sentence)
                    .Args[1].ShouldBe("\"Y\"");
            }

            [Test]
            public void should_create_steps_with_numeric_args()
            {
                const string Sentence = @"The pin should be ""234"" "
                    + @"and the amount should be ""450.23""";

                The.StepFrom(Sentence)
                    .Args[0].ShouldBe("234");

                The.StepFrom(Sentence)
                    .Args[1].ShouldBe("450.23");
            }

            [Test]
            public void should_create_steps_with_date_args()
            {
                const string Sentence = @"The user gets paid on ""02/08/1986""";

                The.StepFrom(Sentence)
                    .Args[0].ShouldBe(@"DateTime.Parse(""02/08/1986"")");
            }
        }

        [TestFixture]
        public class The_generator : BehaviorOf<RunnerGenerator>
        {
            [Test]
            public void should_pass_the_parameters_into_the_step_call()
            {
                The.RunnerFor(Actors.FeatureWithArgs)
                    .ShouldContain(@"If__happens(""X"");");

                The.RunnerFor(Actors.FeatureWithArgs)
                    .ShouldContain(@"If__and__happens(""X"", ""Y"");");
            }
        }
    }
}
