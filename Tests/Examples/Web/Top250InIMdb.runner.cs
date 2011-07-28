using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raconteur.Web;

namespace Examples.Web 
{
    [TestClass]
    public partial class Find250MoviesInIMdb 
    {
        public Browser Browser = new Browser();

        
        [TestMethod]
        public void Find250Movies()
        {         
            Browser.Visit("http://www.imdb.com");        
            Browser.Click_on__with_text("//a", "Top 250");        
            Browser.Title_should_be("IMDb Top 250");        
            Browser.Find_link_with("Pulp Fiction");        
            Browser.Find_link_with("The Matrix");        
            Browser.Find_link_with("Memento");
        }
        
        [TestMethod]
        public void ScoresOfMyFavoriteMovies()
        {         
            Browser.Visit("http://www.imdb.com/chart/top");        
            My_favorite_movies_should_be_there(5, 8.9, "Pulp Fiction");        
            My_favorite_movies_should_be_there(22, 8.7, "The Matrix");        
            My_favorite_movies_should_be_there(31, 8.6, "Memento");
        }

    }
}
