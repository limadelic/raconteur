using System.Collections.Generic;
using System.Linq;

namespace Examples.TicTacToe
{
    public class TicTacToeGame
    {
        public string[][] BoardState { get; set; }

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

        public string Winner
        { 
            get
            {
                return 
                (
                    from line in Lines
                    where IsFilled(line[0]) && line[0] == line[1] && line[1] == line[2]
                    select line[0]
                )
                .FirstOrDefault();
            }
        }

        public TicTacToeGame()
        {
            BoardState = new[]
            {
                new[] { " ", " ", " "},
                new[] { " ", " ", " "},
                new[] { " ", " ", " "},
            };
        }

        public TicTacToeGame(string[][] board) { BoardState = board; }

        public void PlayMove(int row, int column)
        {
            BoardState[row][column] = CurrentPlayer;
        }

        public bool IsFilled(string square)
        {
            return square == "X" || square == "O";
        }

        protected IEnumerable<string[]> Lines
        {
            get
            {
                for (var i = 0; i < 3; i++)
                {
                    yield return new[] { BoardState[i][0], BoardState[i][1], BoardState[i][2] };
                    yield return new[] { BoardState[0][i], BoardState[1][i], BoardState[2][i] };
                }

                yield return new[] { BoardState[0][0], BoardState[1][1], BoardState[2][2] };
                yield return new[] { BoardState[2][0], BoardState[1][1], BoardState[0][2] };
            }
        }
    }
}