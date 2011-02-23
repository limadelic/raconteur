using System;
using System.Collections.Generic;

namespace Raconteur
{
    public class Feature 
    {
        public List<Scenario> Scenarios { get; set; }
        public string Name { get; set; }
        public string Assembly { get; set; }
        public string Namespace { get; set; }
        public string FileName { get; set; }
        public Type StepLibrary { get; set; }

        public Feature()
        {
            Scenarios = new List<Scenario>();
        }
    }

    public class InvalidFeature : Feature
    {
        public string Reason { get; set; }
    }
}