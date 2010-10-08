using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples 
{
    [TestClass]
    public partial class CellConfigurationsThatAreStatic 
    {
        
        [TestMethod]
        public void Block()
        {         
            Given_the_following_setup(".", ".", ".", ".", ".");        
            Given_the_following_setup(".", "x", "x", ".", ".");        
            Given_the_following_setup(".", "x", "x", ".", ".");        
            Given_the_following_setup(".", ".", ".", ".", ".");        
            Given_the_following_setup(".", ".", ".", ".", ".");        
            When_I_evolve_the_board();        
            Then_I_should_see_the_following_board(".", ".", ".", ".", ".");        
            Then_I_should_see_the_following_board(".", "x", "x", ".", ".");        
            Then_I_should_see_the_following_board(".", "x", "x", ".", ".");        
            Then_I_should_see_the_following_board(".", ".", ".", ".", ".");        
            Then_I_should_see_the_following_board(".", ".", ".", ".", ".");
        }
        
        [TestMethod]
        public void Beehive()
        {         
            Given_the_following_setup(".", ".", ".", ".", ".", ".");        
            Given_the_following_setup(".", ".", "x", "x", ".", ".");        
            Given_the_following_setup(".", "x", ".", ".", "x", ".");        
            Given_the_following_setup(".", ".", "x", "x", ".", ".");        
            Given_the_following_setup(".", ".", ".", ".", ".", ".");        
            When_I_evolve_the_board();        
            Then_I_should_see_the_following_board(".", ".", ".", ".", ".", ".");        
            Then_I_should_see_the_following_board(".", ".", "x", "x", ".", ".");        
            Then_I_should_see_the_following_board(".", "x", ".", ".", "x", ".");        
            Then_I_should_see_the_following_board(".", ".", "x", "x", ".", ".");        
            Then_I_should_see_the_following_board(".", ".", ".", ".", ".", ".");
        }
        
        [TestMethod]
        public void Loaf()
        {         
            Given_the_following_setup(".", ".", ".", ".", ".", ".");        
            Given_the_following_setup(".", ".", "x", "x", ".", ".");        
            Given_the_following_setup(".", "x", ".", ".", "x", ".");        
            Given_the_following_setup(".", ".", "x", ".", "x", ".");        
            Given_the_following_setup(".", ".", ".", "x", ".", ".");        
            Given_the_following_setup(".", ".", ".", ".", ".", ".");        
            When_I_evolve_the_board();        
            Then_I_should_see_the_following_board(".", ".", ".", ".", ".", ".");        
            Then_I_should_see_the_following_board(".", ".", "x", "x", ".", ".");        
            Then_I_should_see_the_following_board(".", "x", ".", ".", "x", ".");        
            Then_I_should_see_the_following_board(".", ".", "x", ".", "x", ".");        
            Then_I_should_see_the_following_board(".", ".", ".", "x", ".", ".");        
            Then_I_should_see_the_following_board(".", ".", ".", ".", ".", ".");
        }
        
        [TestMethod]
        public void Boat()
        {         
            Given_the_following_setup(".", ".", ".", ".", ".");        
            Given_the_following_setup(".", "x", "x", ".", ".");        
            Given_the_following_setup(".", "x", ".", "x", ".");        
            Given_the_following_setup(".", ".", "x", ".", ".");        
            Given_the_following_setup(".", ".", ".", ".", ".");        
            When_I_evolve_the_board();        
            Then_I_should_see_the_following_board(".", ".", ".", ".", ".");        
            Then_I_should_see_the_following_board(".", "x", "x", ".", ".");        
            Then_I_should_see_the_following_board(".", "x", ".", "x", ".");        
            Then_I_should_see_the_following_board(".", ".", "x", ".", ".");        
            Then_I_should_see_the_following_board(".", ".", ".", ".", ".");
        }

    }
}
