using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class GenerateRunner 
    {
        
        [TestMethod]
        public void GenerateRunnerClass()
        {         
            When_the_Runner_for_a_Feature_is_generated();        
            Then_it_should_be_a_TestClass();        
            And_it_should_be_named_FeatureName();        
            And_it_should_be_on_the_Feature_Namespace();
        }

    }
}
