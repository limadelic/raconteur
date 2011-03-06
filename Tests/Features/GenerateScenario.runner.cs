using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class GenerateScenario 
    {

        
        [TestMethod]
        public void GenerateTestMethods()
        {         
            Given_the_Feature_contains(
@"
Scenario: Scenario Name
If something happens
Then something else should happen
If something happens
And another thing too
");        
            The_Runner_should_contain(
@"
[TestMethod]
public void ScenarioName()
{
If_something_happens();
Then_something_else_should_happen();
If_something_happens();
And_another_thing_too();
}
");
        }

    }
}
