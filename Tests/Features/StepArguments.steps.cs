using FluentSpec;

namespace Features 
{
    public partial class StepArguments : FeatureRunner
    {
        void When_a_step_contains_arguments()
        {
            Feature = 
            @"
                Feature: Feature Name

                Scenario: Scenario Name
                    If ""X"" happens
            ";
        }
        
        void The_runner_should_pass_them_in_the_call()
        {
            Runner.ShouldContain(@"If__happens(""X"");");
        }

        void When_an_argument_is_an_integer()
        {
            Feature =
            @"
                Feature: Feature Name

                Scenario: Scenario Name
                    If the balance is ""5""
            ";
        }

        void It_should_be_passed_as_a_number()
        {
            Runner.ShouldContain(@"If_the_balance_is(5);");
        }

        void When_an_Arg_is_not_finish_in_a_Sentence()
        {
            Feature =
            @"
                Feature: Feature Name

                Scenario: Multiline Step gherkin
                    Step Arg with multiple lines 
                    ""
                    could start on one line
                    and finish on another
                    ""
            ";
        }

        void It_should_expand_until_it_s_closed_in_another_line()
        {
            Runner.ShouldContain(
            @"
                Step_Arg_with_multiple_lines(
                ""
                could start on one line
                and finish on another
                "");
            ");
        }
    }
}
