using System.Linq;

namespace Examples
{
    public class TicTacToeGame
    {
        public string CurrentPlayer
        {
            get
            {
                var squaresFilled = BoardState.SelectMany(x => x).Count(IsFilled);
                if (squaresFilled % 2 == 0)
                    return "X";

                return "O";
            }
        }

        public string[][] BoardState { get; set; }

        public TicTacToeGame()
        {
            BoardState = new[]
            {
                new[] { " ", " ", " "},
                new[] { " ", " ", " "},
                new[] { " ", " ", " "},
            };
        }

        public void PlayMove(string player, int row, int column)
        {
            BoardState[row][column] = player;
        }

        public bool IsFilled(string square)
        {
            return square == "X" || square == "O";
        }
    }
}