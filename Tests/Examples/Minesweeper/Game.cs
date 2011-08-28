using System.Linq;

namespace Examples.Minesweeper
{
    public enum State
    {
        Dead, Alive, Won
    }
    
    public class Game
    {
        string[][] board;
        string[][] view;

        public string[][] Board
        {
            get
            {
                return Done ? board : view;
            } 
            set
            {
                board = value;

                board.ForEachCell((Row, Col) => 
                    board[Row][Col] = board.HasMineOn(Row, Col) ? "*" : 
                        BombsSurrounding(Row, Col).ToString());

                view = value.Empty();
            }
        }

        bool Done { get { return State != State.Alive; } }

        int BombsSurrounding(int Row, int Col)
        {
            return this
                .AdjacentCells(Row, Col)
                .Sum(x => BombIn(x.Item1, x.Item2));
        }

        int BombIn(int Row, int Col)
        {
            if (OutOfBounds(Row, Col)) return 0;

            return Board.HasMineOn(Row, Col) ? 1 : 0;
        }

        bool OutOfBounds(int Row, int Col)
        {
            return Row < 0 || Row > Board.Length - 1 
                || Col < 0 || Col > Board[0].Length - 1;
        }

        public State State { get; set; }

        public void Click(int Row, int Col)
        {
            State = Board.HasMineOn(Row, Col) ? State.Dead : State.Alive;

            if (Done) return;

            ClickCell(Row, Col);

            CheckIfWon();
        }

        void ClickCell(int Row, int Col) 
        { 
            if (OutOfBounds(Row, Col) || Visited(Row, Col)) return;

            Visit(Row, Col);

            if (board[Row][Col] != "0") return;
            
            this.AdjacentCells(Row, Col)
                .ForEach(x => ClickCell(x.Item1, x.Item2));
        }

        bool Visited(int Row, int Col) { return view[Row][Col] != null; }

        void Visit(int Row, int Col)
        {
            view[Row][Col] = board[Row][Col] == "0" ? "" : board[Row][Col];
        }

        void CheckIfWon()
        {
            if (board.AnyCell((Row, Col) =>
                !Visited(Row, Col) && !board.HasMineOn(Row, Col))) return;

            State = State.Won;
        }
    }
}