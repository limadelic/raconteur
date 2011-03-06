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

    }
}
