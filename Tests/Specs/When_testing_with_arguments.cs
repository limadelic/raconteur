using System;
using System.Collections.Generic;
using System.Linq;
using FluentSpec;
using MbUnit.Framework;
using Raconteur;
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

            [Test]
            public void should_append_a_multiline_Arg_to_the_Step()
            {
                Given.StepFrom("A multiline step arg");
                And.StepFrom("\"");
                And.StepFrom("line 1");
                And.StepFrom("line 2");
                
                When.StepFrom("\"");

                The.LastStep.Name.ShouldBe("A_multiline_step_arg");
                The.LastStep.Args[0].ShouldBe
                (
                    "line 1" + Environment.NewLine + 
                    "line 2" + Environment.NewLine
                );
            }
        }

        [TestFixture]
        public class a_scenario_tokenizer : BehaviorOf<ScenarioTokenizerClass>
        {
            [Test]
            public void should_respect_empty_lines_inside_Multiline_Args()
            {
                Given.Content = 
                @"
                    Scenario: Name
                        Step with multiline Arg with empty line
                        ""

                        ""
                ";

                Then.ScenarioDefinitions[0]
                    .Count().ShouldBe(5);
            }

            [Test]
            public void should_ignore_content_inside_Args()
            {
                Given.Content = 
                @"
                    Scenario: Name
                        Step with multiline Arg with Scenario declaration
                        ""
                            Scenario: That should not be interpreted
                        ""
                ";

                Then.ScenarioDefinitions.Count().ShouldBe(1);
            }
        }

        [TestFixture]
        public class a_scenario_parser : BehaviorOf<ScenarioParserClass>
        {
            [Test]
            public void should_ignore_content_inside_Args()
            {
                Given.Definition = new List<string>
                {
                    "Scenario: Name", 
                        "Step with multiline Arg with Examples declaration",
                        "\"",
                        "Examples:",
                        "\"",
                };
                And.StepParser.StepFrom(null)
                    .IgnoringArgs().WillReturn(new Step());
                
                The.Steps.Count().ShouldBe(4);
            }
        }

        [Test]
        public void should_pass_the_parameters_into_the_step_call()
        {
            var Runner = ObjectFactory.NewRunnerGenerator(Actors.FeatureWithArgs).Code;
            
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

            [Test]
            public void should_calculate_the_arg_boundaries()
            {
                @"I ""was"" eating ""some"" pizza".ArgBoundaries().Count.ShouldBe(2);
                @"I ""was"" eating ""some"" pizza".ArgBoundaries()[0].Start.ShouldBe(2);
                @"I ""was"" eating ""some"" pizza".ArgBoundaries()[0].End.ShouldBe(6);
                @"I ""was"" eating ""some"" pizza".ArgBoundaries()[0].Length.ShouldBe(5);

                @"""Somebody"" said ""I was".ArgBoundaries().Count.ShouldBe(2);
                @"""Somebody"" said ""I was".ArgBoundaries()[1].Start.ShouldBe(16);

                @"Open quote is end of String """.ArgBoundaries()[0].Length.ShouldBe(1);
            }
        }
    }
}
