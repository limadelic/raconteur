using MbUnit.Framework;
using Features.StepDefinitions;

namespace Features 
{
    [TestFixture]
    public partial class TableTypes 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [Test]
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
        
        [Test]
        public void ObjectTableWithSingleRowBecomesAnObjectArg()
        {         
            FeatureRunner.Given_the_Feature_is(
@"
Feature: Table Types
Scenario: Login User
Given the User:
[UserName|Password]
|neo	 |53cr3t  |
");        
            FeatureRunner.The_Runner_should_contain(
@"
[TestMethod]
public void LoginUser()
{
Given_the_User_(
new Common.User
{
UserName = ""neo"",
Password = ""53cr3t""
});
}
");
        }
        
        [Test]        
        [Category("wip")]
        public void ObjectTableWithMultipleRowsBecomesAnObject__Arg()
        {         
            FeatureRunner.Given_the_Feature_is(
@"
Feature: Table Types
Scenario: Login User
Given the Users:
[user name|password]
|neo	  |53cr3t  |
|lola	  |run	   |
");        
            FeatureRunner.The_Runner_should_contain(
@"
[TestMethod]
public void LoginUser()
{
Given_the_Users_(
new Common.User
{
UserName = ""neo"",
Password = ""53cr3t""
},
new Common.User
{
UserName = ""lola"",
Password = ""run""
});
}
");
        }

    }
}
