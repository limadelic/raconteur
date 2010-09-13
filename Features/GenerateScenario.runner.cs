using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class GenerateScenario 
    {
        
        [TestMethod]
        public void GenerateTestMethods()
        {         
            When_the_Scenario_for_a_feature_is_generated();        
            Then_it_should_be_a_Test_Method();        
            And_it_should_be_named_After_the_Scenario_name();
        }

    }
}
