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
    }
}
