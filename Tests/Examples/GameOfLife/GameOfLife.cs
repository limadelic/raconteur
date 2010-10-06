using System.Collections.Generic;
using System.Linq;

namespace Examples
{
    public class GameOfLife
    {
        private List<List<bool>> Board;

        public GameOfLife()
        {
            Board = new List<List<bool>>();
        }

        public void AddRow(params bool[] Row)
        {
            Board.Add(Row.ToList());
        }

        public void Evolve()
        {          
            var newTable = new List<List<bool>>();

            for (var row = 0; row < RowCount; row++)
            {
                newTable.Add(new List<bool>());
                for (var col = 0; col < ColumnCount; col++)
                    newTable[row].Add(CalculateCell(row, col));
            }

            Board = newTable;
        }

        public bool CellIsAlive(int row, int col)
        {
            return !CellIsOutOfRange(row, col) && Board[row][col];
        }

        private bool CellIsOutOfRange(int row, int col)
        {
            return row < 0 || col < 0 || row >= RowCount || col >= ColumnCount;
        }

        public int RowCount
        {
            get { return Board.Count; }
        }

        public int ColumnCount
        {
            get { return Board[0].Count; }
        }

        private bool CalculateCell(int row, int col)
        {
            var aliveNeighborCount = GetNeighbors(row, col).Count(n => n);
            var cellIsAliveInCurrentGeneration = CellIsAlive(row, col);

            return ShouldLiveInNextGeneration(cellIsAliveInCurrentGeneration, aliveNeighborCount);
        }

        private static bool ShouldLiveInNextGeneration(bool cellIsAlive, int liveNeighbors)
        {
            if (cellIsAlive)
                return liveNeighbors == 2 || liveNeighbors == 3;

            return liveNeighbors == 3;
        }

        private IEnumerable<bool> GetNeighbors(int row, int col)
        {
            yield return CellIsAlive(row - 1, col - 1);
            yield return CellIsAlive(row - 1, col);
            yield return CellIsAlive(row - 1, col + 1);

            yield return CellIsAlive(row, col - 1);
            yield return CellIsAlive(row, col + 1);

            yield return CellIsAlive(row + 1, col - 1);
            yield return CellIsAlive(row + 1, col);
            yield return CellIsAlive(row + 1, col + 1);
        }
    }
}