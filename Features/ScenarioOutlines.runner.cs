using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class ScenarioOutlines 
    {
        
        [TestMethod]
        public void UsingScenarioOutlines()
        {         
            When_a_Scenario_has_Outline_Args();        
            It_should_run_one_Test_for_each_row_of_the_Table();
        }

    }
}
