using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples 
{
    [TestClass]
    public partial class EvolvingADeadCell 
    {
        
        [TestMethod]
        public void DeadCellWith0NeighborsStaysDead()
        {         
            Given_the_following_setup(".", ".", ".");        
            Given_the_following_setup(".", ".", ".");        
            Given_the_following_setup(".", ".", ".");        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be_dead();
        }
        
        [TestMethod]
        public void DeadCellWith1NeighborStaysDead()
        {         
            Given_the_following_setup(".", "x", ".");        
            Given_the_following_setup(".", ".", ".");        
            Given_the_following_setup(".", ".", ".");        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be_dead();
        }
        
        [TestMethod]
        public void DeadCellWith2NeighborsStaysDead()
        {         
            Given_the_following_setup(".", "x", ".");        
            Given_the_following_setup(".", ".", "x");        
            Given_the_following_setup(".", ".", ".");        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be_dead();
        }
        
        [TestMethod]
        public void DeadCellWith3NeighborsComesToLife()
        {         
            Given_the_following_setup("x", "x", "x");        
            Given_the_following_setup(".", ".", ".");        
            Given_the_following_setup(".", ".", ".");        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be_alive();
        }
        
        [TestMethod]
        public void DeadCellWith4NeighborsStaysDead()
        {         
            Given_the_following_setup("x", "x", "x");        
            Given_the_following_setup(".", ".", "x");        
            Given_the_following_setup(".", ".", ".");        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be_dead();
        }
        
        [TestMethod]
        public void DeadCellWith5NeighborsStaysDead()
        {         
            Given_the_following_setup("x", "x", "x");        
            Given_the_following_setup("x", ".", "x");        
            Given_the_following_setup(".", ".", ".");        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be_dead();
        }
        
        [TestMethod]
        public void DeadCellWith6NeighborsStaysDead()
        {         
            Given_the_following_setup("x", "x", "x");        
            Given_the_following_setup("x", ".", "x");        
            Given_the_following_setup("x", ".", ".");        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be_dead();
        }
        
        [TestMethod]
        public void DeadCellWith7NeighborsStaysDead()
        {         
            Given_the_following_setup("x", "x", "x");        
            Given_the_following_setup("x", ".", "x");        
            Given_the_following_setup("x", "x", ".");        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be_dead();
        }
        
        [TestMethod]
        public void DeadCellWith8NeighborsStaysDead()
        {         
            Given_the_following_setup("x", "x", "x");        
            Given_the_following_setup("x", ".", "x");        
            Given_the_following_setup("x", "x", "x");        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be_dead();
        }

    }
}
