using System;
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

            var FeatureName = Match.Groups[1].Value;

            return FeatureName.Split(new [] {' '})
                .Aggregate("", (Result, Current) => 
                    Result + Current.Capitalize());

            return FeatureName.Replace(" ", string.Empty);
        }}

    }
}