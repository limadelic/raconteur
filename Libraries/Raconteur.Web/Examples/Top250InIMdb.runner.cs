using MbUnit.Framework;
using Raconteur.Web;

namespace Raconteur.Web.Examples 
{
    [TestFixture]
    public partial class Find250MoviesInIMdb 
    {
        public Browser Browser = new Browser();

        
        [Test]
        public void Find250Movies()
        {         
            Browser.Use("Firefox");        
            Browser.Visit("http://www.imdb.com");        
            Browser.Click_on__with_text("//a", "Top 250");        
            Browser.Title_should_be("IMDb Top 250");        
            Browser.Find_link_with("Pulp Fiction");        
            Browser.Find_link_with("The Matrix");        
            Browser.Find_link_with("Memento");        
            Browser.End();
        }

    }
}
