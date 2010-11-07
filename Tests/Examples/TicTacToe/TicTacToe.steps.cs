using System;
using System.Linq;
using FluentSpec;

namespace Examples 
{
    public partial class TicTacToeRules 
    {
        private TicTacToeGame Game;
        private readonly string[] RowLegend = new[] {"top", "middle", "bottom"};
        private readonly string[] ColumnLegend = new[] { "left", "middle", "right" };

        private void When_a_game_is_started()
        {
            Game = new TicTacToeGame();
        }

        private void The_current_player_should_be(string player)
        {
            Game.CurrentPlayer.ShouldBe(player);
        }

        private void And__plays_in_the(string player, string row, string column)
        {
            var rowIndex = Array.IndexOf(RowLegend, row);
            var columnIndex = Array.IndexOf(ColumnLegend, column);

            Game.PlayMove(player, rowIndex, columnIndex);
        }

        private void And_the_board_state_should_be(string[] topRow, string[] middleRow, string[] bottomRow)
        {
            Game.BoardState[0].ShouldBe(topRow.Select(x => x == "" ? " " : x).ToArray());
            Game.BoardState[1].ShouldBe(middleRow.Select(x => x == "" ? " " : x).ToArray());
            Game.BoardState[2].ShouldBe(bottomRow.Select(x => x == "" ? " " : x).ToArray());
        }
    }
    
    static class ShouldAssertions
    {
        public static void ShouldBe(this object[] One, object[] Another)
        {
            One.Length.ShouldBe(Another.Length, "Arrays are not the same length.");

            for (var i = 0; i < One.Length; i++)
                One[i].ShouldBe(Another[i]);
        }
    }
}
