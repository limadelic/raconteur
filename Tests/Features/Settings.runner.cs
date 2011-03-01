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
using: StepLibrary
using: Another Step Library
");        
            When_the_project_is_loaded();        
            The_Settings_should_be_("MbUnit", "Spanish");        
            The_Step_Libraries_should_be_
            (        
                new[] {"StepLibrary"},        
                new[] {"AnotherStepLibrary"}
            );
        }

    }
}
