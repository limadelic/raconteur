using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class Multilingual 
    {
        
        [TestMethod]
        public void QueVivaEspaña_YOlé_()
        {         
            Select_language("es");        
            Given_the_Feature_is(
@"Característica: Multilingue

Escenario: En Español
");        
            The_Runner_should_contain(
@"[TestClass]
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
