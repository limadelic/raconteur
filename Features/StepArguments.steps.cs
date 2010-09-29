using System;
using FluentSpec;
using Raconteur;
using Raconteur.Parsers;

namespace Features 
{
    public partial class StepArguments : FeatureRunner
    {
        StepParserClass StepParser = new StepParserClass();
        private Step CurrentStep;

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

        private void When_an_argument_is_a_string()
        {
            Feature =
            @"
                Feature: Feature Name

                Scenario: Scenario Name
                    If ""something"" happens
            ";
        }

        private void It_should_be_passed_as_a_string()
        {
            Runner.ShouldContain(@"If__happens(""something"");");
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

        private void When_an_argument_is_a_date()
        {
            Feature =
            @"
                Feature: Feature Name

                Scenario: Scenario Name
                    If the user is paid on ""02/08/1986""
            ";
        }

        private void It_should_be_passed_as_a_date()
        {
            Runner.ShouldContain(
                @"If_the_user_is_paid_on(DateTime.Parse(""02/08/1986"")");
        }
    }
}
