using MbUnit.Framework;
using Raconteur.Web;

namespace Raconteur.Web 
{
    [TestFixture]
    public partial class FindRaconteur 
    {
        public Browser Browser = new Browser();

        
        [Test]
        public void UsingFirefox()
        {         
            Browser.Use("Firefox");        
            Browser.Visit("http://google.com");        
            Browser.Set__to("q", "Raconteur");        
            Browser.Title_should_be("raconteur - Google Search");        
            Browser.End();
        }
        
        [Test]
        public void UsingChrome()
        {         
            Browser.Use("Chrome");        
            Browser.Visit("http://google.com");        
            Browser.Set__to("q", "Raconteur");        
            Browser.Title_should_be("raconteur - Google Search");        
            Browser.End();
        }

    }
}
