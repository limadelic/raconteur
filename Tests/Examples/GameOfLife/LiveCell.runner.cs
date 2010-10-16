using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples 
{
    [TestClass]
    public partial class EvolvingALivingCell 
    {
        
        [TestMethod]
        public void LivingCellWith0NeighborsDies()
        {         
            Given_the_following_setup
            (        
                new[] {".", ".", "."},        
                new[] {".", "x", "."},        
                new[] {".", ".", "."}
            );        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be("dead");
        }
        
        [TestMethod]
        public void LivingCellWith1NeighborDies()
        {         
            Given_the_following_setup
            (        
                new[] {".", "x", "."},        
                new[] {".", "x", "."},        
                new[] {".", ".", "."}
            );        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be("dead");
        }
        
        [TestMethod]
        public void LivingCellWith2NeighborsLives()
        {         
            Given_the_following_setup
            (        
                new[] {".", "x", "."},        
                new[] {".", "x", "x"},        
                new[] {".", ".", "."}
            );        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be("alive");
        }
        
        [TestMethod]
        public void LivingCellWith3NeighborsLives()
        {         
            Given_the_following_setup
            (        
                new[] {"x", "x", "x"},        
                new[] {".", "x", "."},        
                new[] {".", ".", "."}
            );        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be("alive");
        }
        
        [TestMethod]
        public void LivingCellWith4NeighborsDies()
        {         
            Given_the_following_setup
            (        
                new[] {"x", "x", "x"},        
                new[] {".", "x", "x"},        
                new[] {".", ".", "."}
            );        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be("dead");
        }
        
        [TestMethod]
        public void LivingCellWith5NeighborsDies()
        {         
            Given_the_following_setup
            (        
                new[] {"x", "x", "x"},        
                new[] {"x", "x", "x"},        
                new[] {".", ".", "."}
            );        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be("dead");
        }
        
        [TestMethod]
        public void LivingCellWith6NeighborsDies()
        {         
            Given_the_following_setup
            (        
                new[] {"x", "x", "x"},        
                new[] {"x", "x", "x"},        
                new[] {"x", ".", "."}
            );        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be("dead");
        }
        
        [TestMethod]
        public void LivingCellWith7NeighborsDies()
        {         
            Given_the_following_setup
            (        
                new[] {"x", "x", "x"},        
                new[] {"x", "x", "x"},        
                new[] {"x", "x", "."}
            );        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be("dead");
        }
        
        [TestMethod]
        public void LivingCellWith8NeighborsDies()
        {         
            Given_the_following_setup
            (        
                new[] {"x", "x", "x"},        
                new[] {"x", "x", "x"},        
                new[] {"x", "x", "x"}
            );        
            When_I_evolve_the_board();        
            Then_the_center_cell_should_be("dead");
        }

    }
}
