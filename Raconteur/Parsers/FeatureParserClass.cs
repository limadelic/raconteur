using System;
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

            return new Feature
            {
                Name = Name
            };
        }

        string Name { get 
        {
            var Regex = new Regex(@"Feature:(.+)(" + 
                Environment.NewLine + "|$)");
            
            var Match = Regex.Match(Content);

            return Match.Groups[1].Value.CammelCase();
        }}

    }
}