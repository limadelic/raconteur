using MbUnit.Framework;
using Features.StepDefinitions;

namespace Features 
{
    [TestFixture]
    public partial class Comments 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [Test]
        public void SingleLineComments()
        {         
            FeatureRunner.Given_the_Feature_contains_a(
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
        
        [Test]
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
        
        [Test]
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
