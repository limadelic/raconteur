using System;
using System.Collections.Generic;

namespace Raconteur
{
    public class Feature 
    {
        public List<Scenario> Scenarios { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string FileName { get; set; }

        public bool HasStepLibraries
        {
            get { return StepLibraries.HasItems(); }
        }
        public List<Type> StepLibraries { get; set; }

        public Feature()
        {
            Scenarios = new List<Scenario>();
            StepLibraries = new List<Type>();
        }
    }

    public class InvalidFeature : Feature
    {
        public string Reason { get; set; }
    }
}