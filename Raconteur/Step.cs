using System;
using System.Collections.Generic;

namespace Raconteur
{
    public class Step
    {
        public string Name { get; set; }
        public List<string> Args { get; set; }
        public bool Skip { get; set; }
        public Table Table { get; set; }

        public bool HasTable { get { return Table.Rows.Count != 0; } }

        public Step()
        {
            Table = new Table();
            Args = new List<string>();
        }
    }
}