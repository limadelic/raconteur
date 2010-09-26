using System.Collections.Generic;

namespace Raconteur
{
    public class Scenario
    {
        public string Name { get; set; }
        public List<Step> Steps { get; set; }

        public Scenario()
        {
            Steps = new List<Step>();
        }
    }
}