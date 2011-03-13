using MbUnit.Framework;
using Features.StepDefinitions;

namespace Features 
{
    [TestFixture]
    public partial class InvalidFeatures 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [Test]
        public void EmptyFeature()
        {         
            Given_the_Feature_is_Empty();        
            FeatureRunner.The_Runner_should_be("Feature file is Empty");
        }
        
        [Test]
        public void UnparseableFeature()
        {         
            FeatureRunner.Given_the_Feature_is("unparseable feature");        
            FeatureRunner.The_Runner_should_be("Missing Feature declaration");
        }
        
        [Test]
        public void UnnamedFeature()
        {         
            FeatureRunner.Given_the_Feature_is("Feature:");        
            FeatureRunner.The_Runner_should_be("Missing Feature Name");
        }

    }
}
