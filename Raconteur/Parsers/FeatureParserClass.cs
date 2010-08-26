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

            return (from Match Match in Matches 
                             select new Scenario
                                    { Name = 
                                        Match.Groups[1].Value.CamelCase() })
                             .ToList();
        }

        string Name { get 
        {
            var Regex = new Regex(@"Feature: (\w.+)(" + 
                Environment.NewLine + "|$)");
            
            var Match = Regex.Match(Content);

            return Match.Groups[1].Value.CamelCase();
        }}

    }
}