using Microsoft.VisualStudio.TestTools.UnitTesting;
using Features.StepDefinitions;

namespace Features 
{
    [TestClass]
    public partial class Multilingual 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();

        
        [TestMethod]
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
