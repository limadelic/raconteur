using System.Collections.Generic;

namespace Raconteur
{
    public class Table
    {
        public List<List<string>> Rows { get; set; }
        public List<string> Header { get { return Rows[0]; } }

        public int Count { get { return Rows.Count - 1; } }

        public bool HasHeader { get; set; }

        public string this[int Row, int Col]
        {
            get { return Rows[Row + 1][Col]; }
            set { Rows[Row + 1][Col] = value; }
        }

        public Table()
        {
            Rows = new List<List<string>>();
        }

        public void Add(List<string> Row)
        {
            Rows.Add(Row);
        }
    }
}