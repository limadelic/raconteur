
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples.Minesweeper 
{
    public partial class MineSweeper 
    {
        Game Game;

        [TestInitialize]
        public void SetUp()
        {
            Game = new Game();
        }

        void Given_the_board(params string[][] Board)
        {
            Game.Board = Board;
        }

        void When_I_click_on__(int Row, int Col)
        {
            Game.Click(Row, Col);   
        }

        void I_should_see(params string[][] Board)
        {
            Board.ShouldBe(Game.Board);
        }

        void and_I_should_see(params string[][] Board)
        {
            I_should_see(Board);            
        }

        #region State

        void I_should_be(State State)
        {
            Game.State.ShouldBe(State);
        }

        void I_should_win()
        {
            I_should_be(State.Won);
        }

        void I_should_lose()
        {
            I_should_be(State.Dead);
        }

        void I_should_be_alive()
        {
            I_should_be(State.Alive);            
        }

        #endregion    
    }
}
