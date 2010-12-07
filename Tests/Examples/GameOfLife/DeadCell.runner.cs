using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples.GameOfLife 
{
    [TestClass]
    public partial class EvolvingADeadCell 
    {
        
        [TestMethod]
        public void DeadCellWith0NeighborsStaysDead()
        {         
            Given_the_following_setup
            (        
                new[] {".", ".", "."},        
                new[] {".", ".", "."},        
                new[] {".", ".", "."}
            );        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be("dead");
        }
        
        [TestMethod]
        public void DeadCellWith1NeighborStaysDead()
        {         
            Given_the_following_setup
            (        
                new[] {".", "x", "."},        
                new[] {".", ".", "."},        
                new[] {".", ".", "."}
            );        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be("dead");
        }
        
        [TestMethod]
        public void DeadCellWith2NeighborsStaysDead()
        {         
            Given_the_following_setup
            (        
                new[] {".", "x", "."},        
                new[] {".", ".", "x"},        
                new[] {".", ".", "."}
            );        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be("dead");
        }
        
        [TestMethod]
        public void DeadCellWith3NeighborsComesToLife()
        {         
            Given_the_following_setup
            (        
                new[] {"x", "x", "x"},        
                new[] {".", ".", "."},        
                new[] {".", ".", "."}
            );        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be("alive");
        }
        
        [TestMethod]
        public void DeadCellWith4NeighborsStaysDead()
        {         
            Given_the_following_setup
            (        
                new[] {"x", "x", "x"},        
                new[] {".", ".", "x"},        
                new[] {".", ".", "."}
            );        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be("dead");
        }
        
        [TestMethod]
        public void DeadCellWith5NeighborsStaysDead()
        {         
            Given_the_following_setup
            (        
                new[] {"x", "x", "x"},        
                new[] {"x", ".", "x"},        
                new[] {".", ".", "."}
            );        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be("dead");
        }
        
        [TestMethod]
        public void DeadCellWith6NeighborsStaysDead()
        {         
            Given_the_following_setup
            (        
                new[] {"x", "x", "x"},        
                new[] {"x", ".", "x"},        
                new[] {"x", ".", "."}
            );        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be("dead");
        }
        
        [TestMethod]
        public void DeadCellWith7NeighborsStaysDead()
        {         
            Given_the_following_setup
            (        
                new[] {"x", "x", "x"},        
                new[] {"x", ".", "x"},        
                new[] {"x", "x", "."}
            );        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be("dead");
        }
        
        [TestMethod]
        public void DeadCellWith8NeighborsStaysDead()
        {         
            Given_the_following_setup
            (        
                new[] {"x", "x", "x"},        
                new[] {"x", ".", "x"},        
                new[] {"x", "x", "x"}
            );        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be("dead");
        }

    }
}
