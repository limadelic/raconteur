using MbUnit.Framework;
using Features.StepDefinitions;

namespace Features 
{
    [TestFixture]
    public partial class Multilingual 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [Test]
        public void QueVivaEspaña_YOlé_()
        {         
            Select_language("es");        
            FeatureRunner.Given_the_Feature_is(
@"
Funcionalidad: Multilingue
Escenario: En Español
");        
            FeatureRunner.The_Runner_should_contain(
@"
[TestClass]
public partial class Multilingue
{
[TestMethod]
public void EnEspañol()
{
}
}
");
        }

    }
}
