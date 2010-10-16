using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples 
{
    [TestClass]
    public partial class EvolvingAGridWithSomeCellsInIt 
    {
        
        [TestMethod]
        public void SparseGridWithNobodyStayingAlive()
        {         
            Given_the_following_setup
            (        
                new[] {".", ".", ".", ".", "."},        
                new[] {".", "x", ".", "x", "."},        
                new[] {".", ".", ".", ".", "."},        
                new[] {".", "x", ".", "x", "."},        
                new[] {".", ".", ".", ".", "."}
            );        
            When_I_evolve_the_board();        
            Then_I_should_see_the_following_board
            (        
                new[] {".", ".", ".", ".", "."},        
                new[] {".", ".", ".", ".", "."},        
                new[] {".", ".", ".", ".", "."},        
                new[] {".", ".", ".", ".", "."},        
                new[] {".", ".", ".", ".", "."}
            );
        }
        
        [TestMethod]
        public void Over_crowdedGrid()
        {         
            Given_the_following_setup
            (        
                new[] {".", ".", ".", ".", "."},        
                new[] {".", "x", "x", "x", "."},        
                new[] {".", "x", "x", "x", "."},        
                new[] {".", "x", "x", "x", "."},        
                new[] {".", ".", ".", ".", "."}
            );        
            When_I_evolve_the_board();        
            Then_I_should_see_the_following_board
            (        
                new[] {".", ".", "x", ".", "."},        
                new[] {".", "x", ".", "x", "."},        
                new[] {"x", ".", ".", ".", "x"},        
                new[] {".", "x", ".", "x", "."},        
                new[] {".", ".", "x", ".", "."}
            );
        }
        
        [TestMethod]
        public void MultipleDeadCellsComingToLife()
        {         
            Given_the_following_setup
            (        
                new[] {".", ".", ".", ".", "."},        
                new[] {".", ".", ".", ".", "."},        
                new[] {".", "x", "x", "x", "."},        
                new[] {".", ".", ".", ".", "."},        
                new[] {".", ".", ".", ".", "."}
            );        
            When_I_evolve_the_board();        
            Then_I_should_see_the_following_board
            (        
                new[] {".", ".", ".", ".", "."},        
                new[] {".", ".", "x", ".", "."},        
                new[] {".", ".", "x", ".", "."},        
                new[] {".", ".", "x", ".", "."},        
                new[] {".", ".", ".", ".", "."}
            );
        }

    }
}
