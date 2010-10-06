using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples 
{
    [TestClass]
    public partial class EvolvingAGridWithSomeCellsInIt 
    {
        
        [TestMethod]
        public void SparseGridWithNobodyStayingAlive()
        {         
            Given_the_following_setup(".", ".", ".", ".", ".");        
            Given_the_following_setup(".", "x", ".", "x", ".");        
            Given_the_following_setup(".", ".", ".", ".", ".");        
            Given_the_following_setup(".", "x", ".", "x", ".");        
            Given_the_following_setup(".", ".", ".", ".", ".");        
            When_I_evolve_the_board();        
            Then_I_should_see_the_following_board(".", ".", ".", ".", ".");        
            Then_I_should_see_the_following_board(".", ".", ".", ".", ".");        
            Then_I_should_see_the_following_board(".", ".", ".", ".", ".");        
            Then_I_should_see_the_following_board(".", ".", ".", ".", ".");        
            Then_I_should_see_the_following_board(".", ".", ".", ".", ".");
        }
        
        [TestMethod]
        public void Over_crowdedGrid()
        {         
            Given_the_following_setup(".", ".", ".", ".", ".");        
            Given_the_following_setup(".", "x", "x", "x", ".");        
            Given_the_following_setup(".", "x", "x", "x", ".");        
            Given_the_following_setup(".", "x", "x", "x", ".");        
            Given_the_following_setup(".", ".", ".", ".", ".");        
            When_I_evolve_the_board();        
            Then_I_should_see_the_following_board(".", ".", "x", ".", ".");        
            Then_I_should_see_the_following_board(".", "x", ".", "x", ".");        
            Then_I_should_see_the_following_board("x", ".", ".", ".", "x");        
            Then_I_should_see_the_following_board(".", "x", ".", "x", ".");        
            Then_I_should_see_the_following_board(".", ".", "x", ".", ".");
        }
        
        [TestMethod]
        public void MultipleDeadCellsComingToLife()
        {         
            Given_the_following_setup(".", ".", ".", ".", ".");        
            Given_the_following_setup(".", ".", ".", ".", ".");        
            Given_the_following_setup(".", "x", "x", "x", ".");        
            Given_the_following_setup(".", ".", ".", ".", ".");        
            Given_the_following_setup(".", ".", ".", ".", ".");        
            When_I_evolve_the_board();        
            Then_I_should_see_the_following_board(".", ".", ".", ".", ".");        
            Then_I_should_see_the_following_board(".", ".", "x", ".", ".");        
            Then_I_should_see_the_following_board(".", ".", "x", ".", ".");        
            Then_I_should_see_the_following_board(".", ".", "x", ".", ".");        
            Then_I_should_see_the_following_board(".", ".", ".", ".", ".");
        }

    }
}
