using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public class DeclareStepDefinitionsRunner 
    {
        readonly StepDefinitions.DeclareStepDefinitions Steps = new StepDefinitions.DeclareStepDefinitions();
        
        [TestMethod]
        public void CreateStepDefinitionsFile()
        { 
            Steps.When_a_Feature_is_declared_for_the_first_time();
            Steps.The_StepDefinitions_file_should_be_created();
            Steps.and_should_have_a_method_per_Step();
        }

    }
}
