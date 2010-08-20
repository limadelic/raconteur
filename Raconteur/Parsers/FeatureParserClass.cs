using System;
using System.Text.RegularExpressions;

namespace Raconteur.Parsers
{
    public class FeatureParserClass : FeatureParser 
    {
        public Feature FeatureFrom(string Content)
        {
            if (Content == null) return new Feature();

            var Regex = new Regex(@"Feature:(.+)(" + 
                Environment.NewLine + "|$)");
            
            var Match = Regex.Match(Content);
            
            return new Feature
            {
                Name = Match.Groups[1].Value.Replace(" ", string.Empty)
            };
        }
    }
}