using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Raconteur.Generators;
using Raconteur.IDE;

namespace Raconteur.Parsers
{
    public class FeatureParserClass : FeatureParser 
    {
        string Content, Assembly;

        public ScenarioTokenizer ScenarioTokenizer { get; set; }
        public TypeResolver TypeResolver { get; set; }

        public Feature FeatureFrom(FeatureFile FeatureFile, FeatureItem FeatureItem)
        {
            Content = FeatureFile.Content.TrimLines();
            Assembly = FeatureItem.Assembly;

            if (IsNotAValidFeature) return InvalidFeature;

            return new Feature
            {
                FileName = FeatureFile.Name,
                Namespace = FeatureItem.DefaultNamespace,
                Name = Name,
                Scenarios = ScenarioTokenizer.ScenariosFrom(Content),
                StepLibraries = StepLibraries
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
                        Content, 
                        Settings.Language.Feature + @": (\w.+)(" + 
                            Environment.NewLine + "|$)"
                    )
                    .Groups[1].Value.CamelCase().ToValidIdentifier();
                } 
                catch { return null; }
            }
        }

        List<Type> StepLibraries
        {
            get 
            {
                var Matches = Regex.Matches
                (
                    Content, 
                    Settings.Language.Using + @" (\w.+)(\r\n|$)"
                );

                if (Matches.Count == 0 && Settings.StepLibraries.IsEmpty()) return null;

                return Matches.Cast<Match>()
                    .Select(Match => Match.Groups[1].Value.CamelCase())
                    .Union(Settings.StepLibraries)
                    .Select(ClassName => TypeResolver.TypeOf(ClassName, Assembly))
                    .ToList();
            }
        }
    }
}