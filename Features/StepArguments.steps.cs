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

        private void When_an_argument_is_an_integer()
        {
            Feature =
            @"
                Feature: Feature Name

                Scenario: Scenario Name
                    If the balance is ""5""
            ";
        }

        private void It_should_be_passed_as_a_number()
        {
            Runner.ShouldContain(@"If_the_balance_is(5);");
        }
    }
}
