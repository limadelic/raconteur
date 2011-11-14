using System.Linq;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Raconteur.Web.Tests 
{
    public partial class Find250MoviesInIMdb
    {
        [TestInitialize]
        public void SetUp()
        {
            Browser.Use("Firefox");
        }

        public void My_favorite_movies_should_be_there(int rank, double rating, string title)
        {
            Browser.FindElementsByXPath("//div[contains(@id, 'main')]//tr").
                First(t => t.Text.StartsWith(string.Format("{0}. {1} {2}", rank, rating, title))).
                ShouldNotBeNull();
        }

        [TestCleanup]
        public void TearDown()
        {
            Browser.End();
        }
    }
}
