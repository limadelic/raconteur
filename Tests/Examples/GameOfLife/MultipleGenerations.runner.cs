using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples 
{
    [TestClass]
    public partial class EvolvingAGridOverMultipleGenerations 
    {
        
        [TestMethod]
        public void CellsComeAliveThenDieOff()
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
            When_I_evolve_the_board();        
            Then_I_should_see_the_following_board(".", ".", ".", ".", ".");        
            Then_I_should_see_the_following_board(".", ".", ".", ".", ".");        
            Then_I_should_see_the_following_board(".", "x", "x", "x", ".");        
            Then_I_should_see_the_following_board(".", ".", ".", ".", ".");        
            Then_I_should_see_the_following_board(".", ".", ".", ".", ".");
        }

    }
}
