using System.Collections.Generic;

namespace Raconteur
{
    public class Scenario
    {
        public string Name { get; set; }
        public List<Step> Steps { get; set; }
        
        public bool IsOutline { get { return Examples != null;  } }
        public Table Examples { get; set; }
        
        public List<string> Tags { get; set; }

        public Scenario()
        {
            Steps = new List<Step>();
            Tags = new List<string>();
        }
    }
}