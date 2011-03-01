using Microsoft.VisualStudio.TestTools.UnitTesting;
using Features;

namespace Features 
{
    [TestClass]
    public partial class Comments 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();

        
        [TestMethod]
        public void SingleLineComments()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: The Doors
// When the doors of perception are cleansed,
// man will see things as they truly are
Infinite
");        
            FeatureRunner.The_Runner_should_contain(
@"
public void TheDoors()
{
Infinite();
}
");
        }
        
        [TestMethod]
        public void MultilineComments()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: The Doors
/*
When the doors of perception are cleansed,
man will see things as they truly are
*/
Infinite
");        
            FeatureRunner.The_Runner_should_contain(
@"
public void TheDoors()
{
Infinite();
}
");
        }
        
        [TestMethod]
        public void CommentsInsideMultilineArgs()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: The Doors
When
""
// the doors of perception are cleansed,
// man will see things as they truly are
""
Infinite
");        
            FeatureRunner.The_Runner_should_contain(
@"
When(
@""
// the doors of perception are cleansed,
// man will see things as they truly are
"");
Infinite();
");
        }

    }
}
