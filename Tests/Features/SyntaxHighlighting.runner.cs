using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class SyntaxHighlighting 
    {
        
        [TestMethod]
        public void ArgHighlighting()
        {         
            When_the_Feature_contains(
@"Step ""should"" have an arg
");
        }

    }
}
