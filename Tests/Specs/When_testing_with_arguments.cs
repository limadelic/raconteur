using FluentSpec;
using MbUnit.Framework;
using Raconteur;
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
                    .Args[0].ShouldBe("X");
            }

            [Test]
            public void should_create_a_Step_with_multiple_Args()
            {
                const string Sentence = @"If ""X"" and ""Y"" happens";

                The.StepFrom(Sentence)
                    .Name.ShouldBe("If__and__happens");

                The.StepFrom(Sentence).Args.Count.ShouldBe(2);
                
                The.StepFrom(Sentence)
                    .Args[1].ShouldBe("Y");
            }
        }

        [Test]
        public void should_pass_the_parameters_into_the_step_call()
        {
            var Runner = new RunnerGenerator(Actors.FeatureWithArgs).Code;
            
            Runner.ShouldContain(@"If__happens(""X"");");

            Runner.ShouldContain(@"If__and__happens(""X"", ""Y"");");
        }

        [TestFixture]
        public class The_arg_formatter
        {
            [Row("234", "234")]
            [Row("450.23", "450.23")]
            [Row("02/08/1986", @"System.DateTime.Parse(""02/08/1986"")")]
            [Row("true", "true")]
            [Row("false", "false")]
            [Row("null", "null")]
            [Row("string", "\"string\"")]
            public void should_format_Args_by_type(string Arg, string Value)
            {
                ArgFormatter.ValueOf(Arg).ShouldBe(Value);
            }
        }
    }
}
