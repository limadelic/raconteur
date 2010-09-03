using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public class GenerateFeatureRunnerRunner 
    {
        readonly StepDefinitions.GenerateFeatureRunner Steps = new StepDefinitions.GenerateFeatureRunner();
        
        [TestMethod]
        public void GenerateRunnerClass()
        { 
            Steps.When_the_Runner_for_a_Feature_is_generated();
            Steps.Then_it_should_be_a_TestClass();
            Steps.And_it_should_be_named_FeatureFileNameRunner();
            Steps.And_it_should_be_on_the_Feature_Namespace();
        }
        
        [TestMethod]
        public void GenerateStepDefinitionReference()
        { 
            Steps.When_the_Runner_for_a_Feature_is_generated();
            Steps.Then_it_should_generate_a_class_reference_named_FeatureName_under_StepDefinitions();
        }

    }
}
