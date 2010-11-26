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
<xUnit>MbUnit</xUnit>
</raconteur>
</configuration>
");        
            When_the_project_is_loaded();        
            The_xUnit_runner_should_be("MbUnit");
        }

    }
}
