using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples.TicTacToe 
{
    [TestClass]
    public partial class TicTacToeRules 
    {
        
        [TestMethod]
        public void XIsFirstPlayer()
        {         
            Given_a_game_is_started();        
            The_current_player_should_be("X");
        }
        
        [TestMethod]
        public void GameStateByTurn()
        {         
            Given_a_game_is_started();        
            When_the___square_is_played("top", "left");        
            The_current_player_should_be("O");        
            And_the_board_state_should_be
            (        
                new[] {"X", "", ""},        
                new[] {"", "", ""},        
                new[] {"", "", ""}
            );        
            When_the___square_is_played("bottom", "right");        
            The_board_state_should_be
            (        
                new[] {"X", "", ""},        
                new[] {"", "", ""},        
                new[] {"", "", "O"}
            );
        }
        
        [TestMethod]
        public void OWins()
        {         
            With_the_following_board
            (        
                new[] {"X", "X", "O"},        
                new[] {"X", "O", ""},        
                new[] {"", "", ""}
            );        
            When_the___square_is_played("bottom", "left");        
            The_winner_should_be("O");
        }
        
        [TestMethod]
        public void XWins()
        {         
            With_the_following_board
            (        
                new[] {"X", "X", "O"},        
                new[] {"X", "X", "O"},        
                new[] {"O", "O", ""}
            );        
            When_the___square_is_played("bottom", "right");        
            The_winner_should_be("X");
        }
        
        [TestMethod]
        public void Cat_sGame()
        {         
            With_the_following_board
            (        
                new[] {"X", "O", "X"},        
                new[] {"O", "O", "X"},        
                new[] {"X", "X", "O"}
            );        
            It_should_be_a_cat_s_game();
        }
        
        [TestMethod]
        public void HorizontalWin()
        {         
            With_the_following_board
            (        
                new[] {"X", "X", "X"},        
                new[] {"X", "O", "O"},        
                new[] {"O", "O", "X"}
            );        
            The_winner_should_be("X");
        }
        
        [TestMethod]
        public void HorizontalWinInMiddle()
        {         
            With_the_following_board
            (        
                new[] {"O", "X", "O"},        
                new[] {"X", "X", "X"},        
                new[] {"X", "O", "O"}
            );        
            The_winner_should_be("X");
        }
        
        [TestMethod]
        public void HorizontalWinInBottom()
        {         
            With_the_following_board
            (        
                new[] {"X", "X", ""},        
                new[] {"X", "O", "X"},        
                new[] {"O", "O", "O"}
            );        
            The_winner_should_be("O");
        }
        
        [TestMethod]
        public void VerticalWinInMiddle()
        {         
            With_the_following_board
            (        
                new[] {"X", "X", "O"},        
                new[] {"O", "X", "O"},        
                new[] {"O", "X", "X"}
            );        
            The_winner_should_be("X");
        }
        
        [TestMethod]
        public void VerticalWinInRight()
        {         
            With_the_following_board
            (        
                new[] {"X", "O", "X"},        
                new[] {"O", "O", "X"},        
                new[] {"O", "X", "X"}
            );        
            The_winner_should_be("X");
        }
        
        [TestMethod]
        public void VerticalWinInLeft()
        {         
            With_the_following_board
            (        
                new[] {"X", "O", "O"},        
                new[] {"X", "O", "X"},        
                new[] {"X", "X", "O"}
            );        
            The_winner_should_be("X");
        }
        
        [TestMethod]
        public void DiagonalWin()
        {         
            With_the_following_board
            (        
                new[] {"X", "O", "O"},        
                new[] {"X", "O", "X"},        
                new[] {"O", "X", ""}
            );        
            The_winner_should_be("O");
        }

    }
}
