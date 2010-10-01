using System;
using System.Collections.Generic;
using System.Linq;

namespace Raconteur.Parsers
{
    public class ScenarioParserClass : ScenarioParser
    {
        readonly char[] NewLine = Environment.NewLine.ToCharArray();

        const string ScenarioDeclaration = "Scenario: ";

        public StepParser StepParser { get; set; }

        string Content;

        public List<Scenario> ScenariosFrom(string Content)
        {
            this.Content = Content;

            return ScenarioDefinitions.Select(Scenario).ToList();
        }

        IEnumerable<string> Lines { get { return 
            
            Content
            .Split(NewLine)
            .SkipWhile(IsNotScenarioDeclaration)
            .Where(Line => !string.IsNullOrWhiteSpace(Line))
            .Select(Line => Line.Trim());    
        }}

        bool IsScenarioDeclaration(string Line)
        {
            return Line.TrimStart().StartsWith(ScenarioDeclaration);
        }

        bool IsNotScenarioDeclaration(string Line)
        {
            return !IsScenarioDeclaration(Line);
        }

        IEnumerable<List<string>> ScenarioDefinitions
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

        string NameFrom(string Line)
        {
            return Line.Replace(ScenarioDeclaration, "").CamelCase();
        }

        Scenario Scenario(List<string> ScenarioDefinition)
        {
            return new Scenario
            {
                Name = NameFrom(ScenarioDefinition.First()),
                Steps = 
                    ScenarioDefinition.Skip(1)
                    .Select(Line => StepParser.StepFrom(Line))
                    .ToList()
                    .Where(Step => Step != null && !Step.Skip)
                    .ToList()
            };
        }
    }
}