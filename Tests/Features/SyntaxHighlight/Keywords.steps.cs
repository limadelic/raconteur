using Features.StepDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features.SyntaxHighlight 
{
    public partial class HighlightKeywords
    {
        [TestInitialize]
        public void SetUp()
        {
            HighlightFeature = new HighlightFeature { FeatureRunner = FeatureRunner };
        }        
    }
}
