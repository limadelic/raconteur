using Microsoft.VisualStudio.TestTools.UnitTesting;
using Features.StepDefinitions;

namespace Features 
{
    [TestClass]
    public partial class TableTypes 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [TestMethod]
        public void SingleColumnTableBecomesAnArrayArg()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Scenario Name
Given some values:
|0|
|1|
");        
            FeatureRunner.The_Runner_should_contain(
@"
[TestMethod]
public void ScenarioName()
{
Given_some_values_
(
new[] {0, 1}
);
}
");
        }
        
        [TestMethod]        
        [TestCategory("wip")]
        public void ObjectTableWithSingleRowBecomesAnObjectArg()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Login User
Given the User:
[user name|password]
|neo	  |53cr3t  |
");        
            FeatureRunner.And_the_Steps_are("TableTypesSteps");        
            FeatureRunner.The_Runner_should_contain(
@"
[TestMethod]
public void LoginUser()
{
Given_the_User
(
new User
{
UserName = ""neo"",
Password = ""53cr3t"",
}
);
}
");
        }
        
        [TestMethod]
        public void ObjectTableWithMultipleRowsBecomesAnObject__Arg()
        { 
        }

    }
}
