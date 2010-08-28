using System;
using System.Text.RegularExpressions;

namespace Raconteur.Parsers
{
    public class FeatureParserClass : FeatureParser 
    {
        string Content;
        public ScenarioParser ScenarioParser { get; set; }

        public Feature FeatureFrom(string Content)
        {
            if (Content == null) return new Feature();
            this.Content = Content;

            return new Feature
            {
                Name = Name,
                Scenarios = ScenarioParser.ScenariosFrom(Content)
            };
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