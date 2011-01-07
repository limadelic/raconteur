using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class IgnoringScenarios 
    {
        
        [TestMethod]
        public void IgnoreAScenario()
        {         
            Given_the_Feature_contains(
@"
@ignore
Scenario: Ignored
");        
            The_Runner_should_contain(
@"
[TestMethod]
[Ignore]
public void Ignored()
{
}
");
        }
        
        [TestMethod]
        public void IgnoreWithAReason()
        { 
        }

    }
}
