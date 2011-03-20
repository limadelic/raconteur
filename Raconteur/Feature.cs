using System;
using System.Collections.Generic;
using Raconteur.Helpers;
using Raconteur.Parsers;

namespace Raconteur
{
    public class Feature 
    {
        public string Content { get; set; }
        public string Header { get { return Content.Header(); } }

        public List<Scenario> Scenarios { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string FileName { get; set; }

        public Type DefaultStepDefinitions { get; set; }
        public bool HasStepDefinitions
        {
            get { return StepDefinitions.HasItems(); }
        }
        public List<Type> StepDefinitions { get; set; }

        public Feature()
        {
            Scenarios = new List<Scenario>();
            StepDefinitions = new List<Type>();
        }
    }

    public class InvalidFeature : Feature
    {
        public string Reason { get; set; }
    }
}