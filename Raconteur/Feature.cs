using System.Collections.Generic;

namespace Raconteur
{
    public class Feature 
    {
        public List<Scenario> Scenarios { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string FileName { get; set; }
    }
}