using Microsoft.VisualStudio.TestTools.UnitTesting;
using Features.StepDefinitions;

namespace Features 
{
    [TestClass]
    public partial class GenerateStepDefinitionsFile 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();

        
        [TestMethod]
        public void CreateStepDefinitionsFile()
        {         
            When_a_Feature_is_declared_for_the_first_time();        
            The_StepDefinitions_file_should_be_created();
        }

    }
}
