using Features.StepDefinitions;
using MbUnit.Framework;

namespace Features.SyntaxHighlight 
{
    public partial class HighlightComments
    {
        [SetUp]
        public void SetUp()
        {
            HighlightFeature = new HighlightFeature { FeatureRunner = FeatureRunner };
        }        
    }
}
