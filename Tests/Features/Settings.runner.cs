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
@"xUnit: MbUnit
language: es
");        
            When_the_project_is_loaded();        
            The_Settings_should_be_("MbUnit", "Spanish");
        }

    }
}
