using Microsoft.VisualStudio.TestTools.UnitTesting;
using Features.StepDefinitions;

namespace Features 
{
    [TestClass]
    public partial class InvalidFeatures 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();

        
        [TestMethod]
        public void EmptyFeature()
        {         
            Given_the_Feature_is_Empty();        
            FeatureRunner.The_Runner_should_be("Feature file is Empty");
        }
        
        [TestMethod]
        public void UnparseableFeature()
        {         
            FeatureRunner.Given_the_Feature_is("unparseable feature");        
            FeatureRunner.The_Runner_should_be("Missing Feature declaration");
        }
        
        [TestMethod]
        public void UnnamedFeature()
        {         
            FeatureRunner.Given_the_Feature_is("Feature:");        
            FeatureRunner.The_Runner_should_be("Missing Feature Name");
        }

    }
}
