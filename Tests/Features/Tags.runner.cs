using Microsoft.VisualStudio.TestTools.UnitTesting;
using Features.StepDefinitions;

namespace Features 
{
    [TestClass]
    public partial class Tags 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();

        
        [TestMethod]
        public void SingleTag()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
@tag
Scenario: Tagged
");        
            FeatureRunner.The_Runner_should_contain(
@"
[TestMethod]
[TestCategory(""tag"")]
public void Tagged()
{
}
");
        }
        
        [TestMethod]
        public void MultipleTags()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
@one tag
@another tag
Scenario: Tagged
");        
            FeatureRunner.The_Runner_should_contain(
@"
[TestCategory(""one tag"")]
[TestCategory(""another tag"")]
");
        }
        
        [TestMethod]
        public void MultipleTagsInOneLine()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
@one_tag @another_tag
Scenario: Tagged
");        
            FeatureRunner.The_Runner_should_contain(
@"
[TestCategory(""one_tag"")]
[TestCategory(""another_tag"")]
");
        }

    }
}
