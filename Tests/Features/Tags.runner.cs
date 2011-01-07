using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class Tags 
    {
        
        [TestMethod]
        public void SingleTag()
        {         
            Given_the_Feature_contains(
@"
@tag
Scenario: Tagged
");        
            The_Runner_should_contain(
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
            Given_the_Feature_contains(
@"
@one tag
@another tag
Scenario: Tagged
");        
            The_Runner_should_contain(
@"
[TestCategory(""one tag"")]
[TestCategory(""another tag"")]
");
        }
        
        [TestMethod]
        public void MultipleTagsInOneLine()
        {         
            Given_the_Feature_contains(
@"
@one_tag @another_tag
Scenario: Tagged
");        
            The_Runner_should_contain(
@"
[TestCategory(""one_tag"")]
[TestCategory(""another_tag"")]
");
        }

    }
}
