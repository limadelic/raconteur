using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raconteur.Web;

namespace Examples.Web 
{
    public partial class FindRaconteur
    {
        [TestInitialize]
        public void SetUp()
        {
//            Browser.Use("Firefox");
        }

        [TestCleanup]
        public void TearDown()
        {
//            Browser.End();
        }
    }
}
