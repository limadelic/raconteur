using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples 
{
    [TestClass]
    public partial class CellConfigurationsThatAreStatic 
    {
        
        [TestMethod]
        public void Block()
        {         
            Given_the_following_setup
            (        
                new[] {".", ".", ".", ".", "."},        
                new[] {".", "x", "x", ".", "."},        
                new[] {".", "x", "x", ".", "."},        
                new[] {".", ".", ".", ".", "."},        
                new[] {".", ".", ".", ".", "."}
            );        
            When_I_evolve_the_board();        
            Then_I_should_see_the_following_board
            (        
                new[] {".", ".", ".", ".", "."},        
                new[] {".", "x", "x", ".", "."},        
                new[] {".", "x", "x", ".", "."},        
                new[] {".", ".", ".", ".", "."},        
                new[] {".", ".", ".", ".", "."}
            );
        }
        
        [TestMethod]
        public void Beehive()
        {         
            Given_the_following_setup
            (        
                new[] {".", ".", ".", ".", ".", "."},        
                new[] {".", ".", "x", "x", ".", "."},        
                new[] {".", "x", ".", ".", "x", "."},        
                new[] {".", ".", "x", "x", ".", "."},        
                new[] {".", ".", ".", ".", ".", "."}
            );        
            When_I_evolve_the_board();        
            Then_I_should_see_the_following_board
            (        
                new[] {".", ".", ".", ".", ".", "."},        
                new[] {".", ".", "x", "x", ".", "."},        
                new[] {".", "x", ".", ".", "x", "."},        
                new[] {".", ".", "x", "x", ".", "."},        
                new[] {".", ".", ".", ".", ".", "."}
            );
        }

        [TestMethod]
        public void Loaf()
        {         
            Given_the_following_setup
            (        
                new[] {".", ".", ".", ".", ".", "."},        
                new[] {".", ".", "x", "x", ".", "."},        
                new[] {".", "x", ".", ".", "x", "."},        
                new[] {".", ".", "x", ".", "x", "."},        
                new[] {".", ".", ".", "x", ".", "."},        
                new[] {".", ".", ".", ".", ".", "."}
            );        
            When_I_evolve_the_board();        
            Then_I_should_see_the_following_board
            (        
                new[] {".", ".", ".", ".", ".", "."},        
                new[] {".", ".", "x", "x", ".", "."},        
                new[] {".", "x", ".", ".", "x", "."},        
                new[] {".", ".", "x", ".", "x", "."},        
                new[] {".", ".", ".", "x", ".", "."},        
                new[] {".", ".", ".", ".", ".", "."}
            );
        }
        
        [TestMethod]
        public void Boat()
        {         
            Given_the_following_setup
            (        
                new[] {".", ".", ".", ".", "."},        
                new[] {".", "x", "x", ".", "."},        
                new[] {".", "x", ".", "x", "."},        
                new[] {".", ".", "x", ".", "."},        
                new[] {".", ".", ".", ".", "."}
            );        
            When_I_evolve_the_board();        
            Then_I_should_see_the_following_board
            (        
                new[] {".", ".", ".", ".", "."},        
                new[] {".", "x", "x", ".", "."},        
                new[] {".", "x", ".", "x", "."},        
                new[] {".", ".", "x", ".", "."},        
                new[] {".", ".", ".", ".", "."}
            );
        }

    }
}
