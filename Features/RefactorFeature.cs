using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public class RefactorFeatureRunner 
    {
        readonly StepDefinitions.RefactorFeature Steps = new StepDefinitions.RefactorFeature();
        
        [TestMethod]
        public void RefactorFeatureName()
        { 
            Steps.Given_an_existing_Runner_and_Definition_file();
            Steps.If_the_Feature_Name_has_changed();
            Steps.The_Definition_class_should_be_refactored();
        }

    }
}
