using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Raconteur.Parsers
{
    public class FeatureParserClass : FeatureParser 
    {
        string Content;

        public Feature FeatureFrom(string Content)
        {
            if (Content == null) return new Feature();
            this.Content = Content;

            var Scenarios = BuildScenarios();

            return new Feature
            {
                Name = Name,
                Scenarios = Scenarios
            };
        }

        private List<Scenario> BuildScenarios()
        {
            var Regex = new Regex(@"Scenario: (\w.+)(" +
                Environment.NewLine + "|$)");

            var Matches = Regex.Matches(Content);

            var Scenarios = (from object Match in Matches select new Scenario()).ToList();

            return Scenarios;
        }

        string Name { get 
        {
            var Regex = new Regex(@"^Feature: (\w.+)(" + 
                Environment.NewLine + "|$)");
            
            var Match = Regex.Match(Content);

            return Match.Groups[1].Value.CamelCase();
        }}

    }
}