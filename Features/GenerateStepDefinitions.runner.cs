using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class GenerateStepDefinitionsFile 
    {
        
        [TestMethod]
        public void CreateStepDefinitionsFile()
        {         
            When_a_Feature_is_declared_for_the_first_time();        
            The_StepDefinitions_file_should_be_created();
        }
        
        [TestMethod]
        public void UpdateFeatureFile()
        {         
            When_the_Feature_file_is_updated();        
            The_StepDefinitions_file_should_not_be_recreated();
        }

    }
}
