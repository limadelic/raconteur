using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples 
{
    [TestClass]
    public partial class TicTacToeRules 
    {
        
        [TestMethod]
        public void XIsFirstPlayer()
        {         
            When_a_game_is_started();        
            The_current_player_should_be("X");
        }
        
        [TestMethod]
        public void GameStateByTurn()
        {         
            When_a_game_is_started();        
            And__plays_in_the("X", "top", "left");        
            The_current_player_should_be("O");        
            And_the_board_state_should_be
            (        
                new[] {"X", "", ""},        
                new[] {"", "", ""},        
                new[] {"", "", ""}
            );
        }

    }
}
