using System.Collections.Generic;

namespace Raconteur
{
    public class Scenario
    {
        public string Name { get; set; }
        public List<string> Steps { get; set; }

        public Scenario()
        {
            Steps = new List<string>();
        }
    }
}