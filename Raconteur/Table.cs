using System.Collections.Generic;

namespace Raconteur
{
    public class Table
    {
        public string Name { get; set; }
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
            if (IsSingleColumn) Rows[0].Add(Row[0]);
            else Rows.Add(Row);
        }

        bool? isSingleColumn;
        bool IsSingleColumn
        {
            get
            {
                return !HasHeader 
                    && Rows.Count > 0 
                    && (isSingleColumn ?? (isSingleColumn = 
                        Rows[0].Count == 1)).Value;
            }
        }
    }
}