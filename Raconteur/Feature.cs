using System;
using System.Collections.Generic;
using System.Linq;
using Raconteur.Helpers;

namespace Raconteur
{
    public class Feature 
    {
        public string FileName { get; set; }
        public string Content { get; set; }

        public string Name { get; set; }
        public string Namespace { get; set; }

        public List<Scenario> Scenarios { get; set; }

        public List<Step> Steps
        {
            get
            {
                return Scenarios.IsEmpty() ? new List<Step>() : 
                    Scenarios.SelectMany(Scenario => Scenario.Steps).ToList();
            }
        }

        public Type DefaultStepDefinitions { get { return StepDefinitions[0]; } }
        public bool HasStepDefinitions
        {
            get { return StepDefinitions.HasItems(); }
        }

        public List<string> DeclaredStepDefinitions { get; set; }
        public List<Type> StepDefinitions { get; set; }

        public Feature()
        {
            Scenarios = new List<Scenario>();
            StepDefinitions = new List<Type>();
            DeclaredStepDefinitions = new List<string>();
        }

        public void Refresh()
        {
            Steps
                .Where(s => s.IsDirty)
                .Reverse()
                .ForEach(Refresh);
        }

        void Refresh(Step Step)
        {
            Content = 
                Content.Substring(0, Step.Location.Start) +
                Step.Name +
                Content.Substring(Step.Location.End);
        }
    }

    public class InvalidFeature : Feature
    {
        public string Reason { get; set; }
    }
}