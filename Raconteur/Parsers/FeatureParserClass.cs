using System;
using System.Text.RegularExpressions;
using Raconteur.Generators;

namespace Raconteur.Parsers
{
    public class FeatureParserClass : FeatureParser 
    {
        string Content;
        public ScenarioParser ScenarioParser { get; set; }

        public Feature FeatureFrom(FeatureFile FeatureFile)
        {
            if (FeatureFile == null) return new Feature();
            Content = FeatureFile.Content;

            return new Feature
            {
                FileName = FeatureFile.Name,
                Namespace = FeatureFile.Namespace,
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