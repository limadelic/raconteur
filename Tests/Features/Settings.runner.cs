using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class UserSettings 
    {

        
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
