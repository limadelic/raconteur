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
        }
        
        [TestMethod]
        public void MultipleTagsInOneLine()
        { 
        }

    }
}
