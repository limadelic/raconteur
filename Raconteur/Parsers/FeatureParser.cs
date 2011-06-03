using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Raconteur.Helpers;
using Raconteur.IDE;

namespace Raconteur.Parsers
{
    public interface FeatureParser 
    {
        Feature FeatureFrom(FeatureFile FeatureFile, FeatureItem FeatureItem);
        Feature FeatureFrom(string FeatureText, FeatureItem FeatureItem);
    }

    public class FeatureParserClass : FeatureParser
    {
        Feature Feature;

        public ScenarioTokenizer ScenarioTokenizer { get; set; }

        public Feature FeatureFrom(FeatureFile FeatureFile, FeatureItem FeatureItem)
        {
            return ParseFeature(FeatureFile.Name, FeatureFile.Content, FeatureItem);
        }

        public Feature FeatureFrom(string FeatureText, FeatureItem FeatureItem)
        {
            return ParseFeature(null, FeatureText, FeatureItem);
        }

        private Feature ParseFeature(string FeatureName, string Content, FeatureItem FeatureItem)
        {
            Feature = new Feature
            {
                FileName = FeatureName,
                Content = Content.TrimLines(),
            };

            if (IsNotAValidFeature) return InvalidFeature;

            Feature.Namespace = FeatureItem.DefaultNamespace;
            Feature.Name = Name;
            Feature.DeclaredStepDefinitions = DeclaredStepDefinitions;

            Feature.Scenarios = ScenarioTokenizer.ScenariosFrom(Feature.Content);
            
            Feature.Steps.ForEach(Step => Step.Feature = Feature);

            return Feature;
        }

        bool IsNotAValidFeature
        {
            get
            {
                return Feature.Content.IsEmpty()
                    || !Feature.Content.StartsWith(Settings.Language.Feature)
                    || Name.IsEmpty();
            }
        }

        Feature InvalidFeature
        {
            get
            {
                return new InvalidFeature
                {
                    Reason = Feature.Content.IsEmpty() ? "Feature file is Empty" : 
                        !Feature.Content.StartsWith(Settings.Language.Feature) ? "Missing Feature declaration" : 
                        "Missing Feature Name"
                };
            }
        }

        string Name 
        { 
            get 
            {
                try
                {
                    return Regex.Match(Header, 
                        RegexExpressions.FeatureDefinition)
                        .Groups[1].Value.CamelCase().ToValidIdentifier();
                } 
                catch { return null; }
            }
        }

        List<string> DeclaredStepDefinitions
        {
            get 
            {
                var Matches = Regex.Matches(Header, 
                    RegexExpressions.UsingStatement);

                if (Matches.Count == 0 && Settings.StepDefinitions.IsEmpty()) 
                    return new List<string>();

                return Matches.Cast<Match>()
                    .Select(Match => Match.Groups[1].Value.CamelCase())
                    .ToList();
            }
        }

        public string Header
        {
            get
            {
                var IndexOfScenario = Feature.Content.IndexOf("\r\n" + Settings.Language.Scenario);

                return Feature.Content.StartsWith(Settings.Language.Scenario) ? string.Empty :
                    IndexOfScenario == -1 ? Feature.Content : 
                    Feature.Content.Substring(0, IndexOfScenario);            
            }
        }
    }
}