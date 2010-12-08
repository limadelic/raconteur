using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class Comments 
    {
        
        [TestMethod]
        public void SingleLineComments()
        {         
            Given_the_Feature_is(
@"Feature: Comments
Scenario: The Doors
// When the doors of perception are cleansed,
// man will see things as they truly are
Infinite
");        
            The_Runner_should_contain(
@"public void TheDoors()
{
Infinite();
}
");
        }
        
        [TestMethod]
        public void MultilineComments()
        { 
        }

    }
}
