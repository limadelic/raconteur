using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Raconteur.Web.Tests 
{
    public partial class FindRaconteur
    {
        [TestCleanup]
        public void TearDown()
        {
            Browser.End();
        }
    }
}
