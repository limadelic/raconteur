using System;
using System.Text.RegularExpressions;
using Raconteur.Generators;
using Raconteur.IDE;

namespace Raconteur.Parsers
{
    public class FeatureParserClass : FeatureParser 
    {
        string Content;
        public ScenarioTokenizer ScenarioTokenizer { get; set; }

        public Feature FeatureFrom(FeatureFile FeatureFile, FeatureItem FeatureItem)
        {
            Content = FeatureFile.Content;

            if (!IsValid) return InvalidFeature;

            return new Feature
            {
                FileName = FeatureFile.Name,
                Namespace = FeatureItem.DefaultNamespace,
                Name = Name,
                Scenarios = ScenarioTokenizer.ScenariosFrom(Content)
            };
        }

        bool IsValid
        {
            get { return !Content.IsEmpty(); }
        }

        Feature InvalidFeature
        {
            get { return new InvalidFeature {Reason = "Feature file is Empty"}; }
        }

        string Name 
        { 
            get 
            {
                var Regex = new Regex(Settings.Language.Feature + 
                    @": (\w.+)(" + Environment.NewLine + "|$)");
            
                var Match = Regex.Match(Content);

                return Match.Groups[1].Value
                    .CamelCase().ToValidIdentifier();
            }
        }
    }
}