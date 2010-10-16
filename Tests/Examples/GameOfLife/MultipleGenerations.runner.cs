using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples 
{
    [TestClass]
    public partial class EvolvingAGridOverMultipleGenerations 
    {
        
        [TestMethod]
        public void CellsComeAliveThenDieOff()
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
            When_I_evolve_the_board();        
            Then_I_should_see_the_following_board
            (        
                new[] {".", ".", ".", ".", "."},        
                new[] {".", ".", ".", ".", "."},        
                new[] {".", "x", "x", "x", "."},        
                new[] {".", ".", ".", ".", "."},        
                new[] {".", ".", ".", ".", "."}
            );
        }

    }
}
