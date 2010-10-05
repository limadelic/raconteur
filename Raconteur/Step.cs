using System.Collections.Generic;

namespace Raconteur
{
    public class Step
    {
        public string Name { get; set; }
        public List<string> Args { get; set; }
        
        public bool HasTable { get { return Table != null; } }
        public Table Table { get; set; }

        public Step()
        {
            Args = new List<string>();
        }

        public void AddRow(List<string> Row)
        {
            if (!HasTable) Table = new Table();

            Table.Add(Row);
        }
    }
}