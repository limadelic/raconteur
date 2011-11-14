using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raconteur.Web;

namespace Raconteur.Web.Tests 
{
    [TestClass]
    public partial class FindRaconteur 
    {
        public Browser Browser = new Browser();

        
        [TestMethod]
        public void UsingFirefox_default_()
        {         
            Browser.Use("Firefox");        
            Browser.Visit("http://google.com");        
            Browser.Set__to("q", "Raconteur");        
            Browser.Title_should_be("raconteur - Google Search");
        }
        
        [TestMethod]
        public void UsingChrome()
        {         
            Browser.Use("Chrome");        
            Browser.Visit("http://google.com");        
            Browser.Set__to("q", "Raconteur");        
            Browser.Title_should_be("raconteur - Google Search");
        }

    }
}
