using System;
using System.Collections.Generic;
using System.Linq;

namespace Raconteur.Parsers
{
    public class ScenarioParserClass : ScenarioParser
    {
        readonly char[] NewLine = Environment.NewLine.ToCharArray();

        const string ScenarioDeclaration = "Scenario: ";

        public List<Scenario> ScenariosFrom(string Content)
        {
            var Definitions = ExtractScenarionDefinitionsFrom(Content);
            
            return Definitions.Select(BuildScenario).ToList();
        }

        bool IsScenarioDeclaration(string Line)
        {
            return Line.TrimStart().StartsWith("Scenario: ");
        }

        IEnumerable<List<string>> ExtractScenarionDefinitionsFrom(string Content)
        {
            var Lines = Content.Split(NewLine).ToList();
            
            var Declarations = Lines.Where(IsScenarioDeclaration).ToList();

            var PreviousDeclaration = -1;

            foreach (var Declaration in Declarations)
            {
                var CurrentDeclaration = Lines.IndexOf(Declaration);

                if (PreviousDeclaration >= 0)
                {
                    yield return Lines.GetRange(PreviousDeclaration, 
                        CurrentDeclaration - PreviousDeclaration);
                }

                PreviousDeclaration = Lines.IndexOf(Declaration);
            }

            if (PreviousDeclaration >= 0)
                yield return Lines.GetRange(PreviousDeclaration, 
                    Lines.Count - PreviousDeclaration);
        }

        string ExtractNameFrom(string Line)
        {
            return Line.Trim()
                .Substring(ScenarioDeclaration.Length - 1)
                .CamelCase();
        }

        Scenario BuildScenario(List<string> ScenarioDefinition)
        {
            var Name = ExtractNameFrom(ScenarioDefinition[0]);
            ScenarioDefinition.RemoveAt(0);

            return new Scenario
            {
                Name = Name,
                Steps = (from Step in ScenarioDefinition 
                         where !string.IsNullOrWhiteSpace(Step)
                         select Step.ToValidIdentifier()).ToList()
            };
        }
    }
}