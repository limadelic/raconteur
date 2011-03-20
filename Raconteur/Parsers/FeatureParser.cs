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
    }

    public class FeatureParserClass : FeatureParser
    {
        string Content;

        public ScenarioTokenizer ScenarioTokenizer { get; set; }

        public Feature FeatureFrom(FeatureFile FeatureFile, FeatureItem FeatureItem)
        {
            Content = FeatureFile.Content.TrimLines();

            if (IsNotAValidFeature) return InvalidFeature;

            return new Feature
            {
                FileName = FeatureFile.Name,
                Namespace = FeatureItem.DefaultNamespace,
                Name = Name,
                Scenarios = ScenarioTokenizer.ScenariosFrom(Content),
                DeclaredStepDefinitions = DeclaredStepDefinitions
            };
        }

        bool IsNotAValidFeature
        {
            get
            {
                return Content.IsEmpty()
                    || !Content.StartsWith(Settings.Language.Feature)
                    || Name.IsEmpty();
            }
        }

        Feature InvalidFeature
        {
            get
            {
                var Reason = 
                    Content.IsEmpty() ? "Feature file is Empty"
                    : !Content.StartsWith(Settings.Language.Feature) ? "Missing Feature declaration"
                    : "Missing Feature Name";

                return new InvalidFeature {Reason = Reason};
            }
        }

        string Name 
        { 
            get 
            {
                try
                {
                    return Regex.Match
                    (
                        Content.Header(), 
                        Settings.Language.Feature + @": (\w.+)(" + 
                            Environment.NewLine + "|$)"
                    )
                    .Groups[1].Value.CamelCase().ToValidIdentifier();
                } 
                catch { return null; }
            }
        }

        List<string> DeclaredStepDefinitions
        {
            get 
            {
                var Matches = Regex.Matches
                (
                    Content.Header(), 
                    Settings.Language.Using + @" (\w.+)(\r\n|$)"
                );

                if (Matches.Count == 0 && Settings.StepDefinitions.IsEmpty()) 
                    return new List<string>();

                return Matches.Cast<Match>()
                    .Select(Match => Match.Groups[1].Value.CamelCase())
                    .ToList();
            }
        }
    }

    public static class FeatureParserClassEx
    {
        public static string Header(this string Feature)
        {
            var IndexOfScenario = Feature.IndexOf("\r\n" + Settings.Language.Scenario);

            return Feature.StartsWith(Settings.Language.Scenario) ? string.Empty :
                IndexOfScenario == -1 ? Feature : 
                Feature.Substring(0, IndexOfScenario);
        }
    }
}