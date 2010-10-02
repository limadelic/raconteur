using System;
using System.Collections.Generic;
using System.Linq;

namespace Raconteur.Parsers
{
    public class ScenarioTokenizerClass : ScenarioTokenizer
    {
        public ScenarioParser ScenarioParser { get; set; }

        string Content;
        public List<Scenario> ScenariosFrom(string Content)
        {
            this.Content = Content;

            return 
            (
                from Definition in ScenarioDefinitions
                select ScenarioParser.ScenarioFrom(Definition)
            ).ToList();
        }

        List<List<string>> ScenarioDefinitions
        {
            get
            {
                var Results = new List<List<string>>();

                foreach (var Line in Lines)
                {
                    if (IsScenarioDeclaration(Line))
                        Results.Add(new List<string>());

                    Results.Last().Add(Line);
                }

                return Results;
            }
        }

        readonly char[] NewLine = Environment.NewLine.ToCharArray();
        const string ScenarioDeclaration = "Scenario";

        IEnumerable<string> Lines
        {
            get
            {
                return Content
                    .Split(NewLine)
                    .SkipWhile(IsNotScenarioDeclaration)
                    .Where(Line => !string.IsNullOrWhiteSpace(Line))
                    .Select(Line => Line.Trim());
            }
        }

        bool IsScenarioDeclaration(string Line)
        {
            return Line.TrimStart().StartsWith(ScenarioDeclaration);
        }

        bool IsNotScenarioDeclaration(string Line)
        {
            return !IsScenarioDeclaration(Line);
        }
    }
}