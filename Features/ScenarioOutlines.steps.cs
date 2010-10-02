using Specs;

namespace Features 
{
    public partial class ScenarioOutlines : FeatureRunner
    {
        void When_a_Scenario_has_Outline_Args()
        {
            Feature =
            @"
                Feature: Feature Name

                Scenario Outline: Interest Rate
                  Given <account> has <amount>
                  When interest is calculated
                  It should be <interest>
                    
                  Examples:
                    |account|amount|interest|
                    |23     |42    |1       |
                    |56     |23    |3       |    
            ";
        }

        void It_should_run_one_Test_for_each_row_of_the_Table()
        {
            Runner.ShouldContainInOrder
            (
                @"public void Outline1()",
                    @"Given__has(23, 42)",
                    @"When_interest_is_calculated()",
                    @"It_should_be(1)",

                @"public void Outline2()",
                    @"Given__has(56, 23)",
                    @"When_interest_is_calculated()",
                    @"It_should_be(3)"
            );
        }
    }
}
