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
@"<configuration>
<raconteur>
<xUnit name=""MbUnit"" />
<language code=""es"" />
</raconteur>
</configuration>
");        
            When_the_project_is_loaded();        
            The_Settings_should_be_("MbUnit", "Spanish");
        }

    }
}
