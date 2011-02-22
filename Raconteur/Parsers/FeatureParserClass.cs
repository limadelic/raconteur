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
        public TypeResolver TypeResolver { get; set; }

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
                StepLibrary = StepLibrary
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

        Type StepLibrary
        {
            get 
            {
                try
                {
                    var ClassName = Regex.Match
                    (
                        Content, 
                        @"using (\w.+)(" + Environment.NewLine + "|$)"
                    )
                    .Groups[1].Value.CamelCase().ToValidIdentifier();

                    return TypeResolver.TypeOf(ClassName);
                } 
                catch { return null; }
            }
        }
    }
}