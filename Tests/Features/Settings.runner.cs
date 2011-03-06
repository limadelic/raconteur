using Microsoft.VisualStudio.TestTools.UnitTesting;
using Features.StepDefinitions;

namespace Features 
{
    [TestClass]
    public partial class UserSettings 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [TestMethod]
        public void SettingTheXUnitRunner()
        {         
            Given_the_settings(
@"
xUnit: MbUnit
language: es
lib: Library
lib: another Library
using: StepDefinitions
using: Another Step Definitions
");        
            When_the_project_is_loaded();        
            The_Settings_should_be_("MbUnit", "Spanish");        
            The_Libraries_should_be_
            (        
                new[] {"Library"},        
                new[] {"another Library"}
            );        
            The_Step_Definitions_should_be_
            (        
                new[] {"StepDefinitions"},        
                new[] {"AnotherStepDefinitions"}
            );
        }

    }
}
