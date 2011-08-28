using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples.Minesweeper 
{
    [TestClass]
    public partial class MineSweeper 
    {

        
        [TestMethod]
        public void StepOnAMineDie()
        {         
            Given_the_board
            (        
                new[] {"*", ""},        
                new[] {"", ""}
            );        
            When_I_click_on__(0, 0);        
            I_should_lose();
        }
        
        [TestMethod]
        public void WhenUDieUSeeTheBoard()
        {         
            Given_the_board
            (        
                new[] {"*", ""},        
                new[] {"", ""}
            );        
            When_I_click_on__(0, 0);        
            I_should_see
            (        
                new[] {"*", "1"},        
                new[] {"1", "1"}
            );
        }
        
        [TestMethod]
        public void WhenYouDieYouSeeTheBoard()
        {         
            Given_the_board
            (        
                new[] {"*", "*"},        
                new[] {"*", ""}
            );        
            When_I_click_on__(0, 0);        
            I_should_see
            (        
                new[] {"*", "*"},        
                new[] {"*", "3"}
            );
        }
        
        [TestMethod]
        public void GotLuckyN_Won()
        {         
            Given_the_board
            (        
                new[] {"", "*"},        
                new[] {"*", "*"}
            );        
            When_I_click_on__(0, 0);        
            I_should_win();        
            and_I_should_see
            (        
                new[] {"3", "*"},        
                new[] {"*", "*"}
            );
        }
        
        [TestMethod]
        public void StepOnClearSpotWith2AdjacentBombs()
        {         
            Given_the_board
            (        
                new[] {"", "*"},        
                new[] {"", "*"}
            );        
            When_I_click_on__(0, 0);        
            I_should_be_alive();        
            and_I_should_see
            (        
                new[] {"2", ""},        
                new[] {"", ""}
            );
        }
        
        [TestMethod]
        public void ClickingAFreeSpaceOpensUpTheEntireBoard()
        {         
            Given_the_board
            (        
                new[] {"", "", ""},        
                new[] {"", "", ""},        
                new[] {"", "", "*"}
            );        
            When_I_click_on__(0, 0);        
            I_should_win();        
            and_I_should_see
            (        
                new[] {"", "", ""},        
                new[] {"", "1", "1"},        
                new[] {"", "1", "*"}
            );
        }

    }
}
