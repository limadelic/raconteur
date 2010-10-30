using FluentSpec;

namespace Features 
{
    public partial class StepArguments : FeatureRunner
    {
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
            Runner.ShouldContain("Step_Arg_with_multiple_lines(");
            Runner.ShouldContain
            (
@"@""could start on one line
and finish on another
"");"
            );
        }
    }
}
