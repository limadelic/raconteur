using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Raconteur.Compilers;
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
        string Header;
        List<string> Assemblies;

        public ScenarioTokenizer ScenarioTokenizer { get; set; }
        public TypeResolver TypeResolver { get; set; }

        public Feature FeatureFrom(FeatureFile FeatureFile, FeatureItem FeatureItem)
        {
            SetUpContext(FeatureFile, FeatureItem);

            if (IsNotAValidFeature) return InvalidFeature;

            return new Feature
            {
                FileName = FeatureFile.Name,
                Namespace = FeatureItem.DefaultNamespace,
                Name = Name,
                Header = Header,
                DefaultStepDefinitions = DefaultStepDefinitions,
                Scenarios = ScenarioTokenizer.ScenariosFrom(Content),
//                StepDefinitions = StepDefinitions
            };
        }

        void SetUpContext(FeatureFile FeatureFile, FeatureItem FeatureItem) 
        {
            Content = FeatureFile.Content.TrimLines();

            Header = Content.Header();

            Assemblies = new List<string> {FeatureItem.Assembly};
            Assemblies.AddRange(Settings.Libraries);
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
                        Header, 
                        Settings.Language.Feature + @": (\w.+)(" + 
                            Environment.NewLine + "|$)"
                    )
                    .Groups[1].Value.CamelCase().ToValidIdentifier();
                } 
                catch { return null; }
            }
        }

        Type DefaultStepDefinitions 
        { 
            get 
            {
                try { return TypeOfStepDefinitions(Name); } 
                catch { return null; }
            }
        }

        Type TypeOfStepDefinitions(string ClassName)
        {
            return Assemblies
                .Select(Assembly => TypeResolver.TypeOf(ClassName, Assembly))
                .FirstOrDefault(Type => Type != null);
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