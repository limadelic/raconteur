namespace Features 
{
    public partial class StepTable : FeatureRunner
    {
        void When_a_table_is_declared()
        {
            Feature = 
            @"
                Feature: Feature Name

                Scenario: Scenario Name
                    Verify some values:
                    |X|Y|
                    |1|0|
                    |0|1|
            ";
        }

        void Each_row_should_become_a_Step_with_cols_as_Args()
        {
            Runner.ShouldContainInOrder(
                @"Verify_some_values(""1"",""0"");",
                @"Verify_some_values(""0"",""1"");");
        }
    }
}
