using MbUnit.Framework;
using Features.StepDefinitions;

namespace Features 
{
    [TestFixture]
    public partial class GenerateStepDefinitionsFile 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [Test]
        public void CreateStepDefinitionsFile()
        {         
            When_a_Feature_is_declared_for_the_first_time();        
            The_StepDefinitions_file_should_be_created();
        }

    }
}
