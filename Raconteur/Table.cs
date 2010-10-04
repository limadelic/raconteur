using System.Collections.Generic;

namespace Raconteur
{
    public class Table
    {
        public List<List<string>> Rows { get; set; }

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