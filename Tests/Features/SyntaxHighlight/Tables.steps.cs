using Features.StepDefinitions;
using MbUnit.Framework;

namespace Features.SyntaxHighlight 
{
    public partial class TableValues
    {
        [SetUp]
        public void SetUp()
        {
            HighlightFeature = new HighlightFeature { FeatureRunner = FeatureRunner };
        }        
    }
}
