using System.Collections.Generic;
using System.Linq;

namespace Raconteur.Parsers
{
    public class ScenarioParserClass : ScenarioParser
    {
        readonly char[] Colon = {':'};

        public StepParser StepParser { get; set; }

        List<string> Definition;

        public Scenario ScenarioFrom(List<string> Definition)
        {
            this.Definition = Definition;

            return new Scenario
            {
                Name = Name,
                Steps = Steps,
                Examples = Examples
            };
        }

        protected Table Examples
        {
            get
            {
                var Rows = Definition
                    .SkipWhile(IsNotExample)
                    .Skip(1)
                    .Select(ParseTableRow).ToList();

                return Rows.Count == 0 ? null : 
                    new Table { Rows = Rows };
            }
        }

        protected bool IsNotExample(string Line)
        {
            return !Line.StartsWith("Examples:");
        }

        List<string> ParseTableRow(string Row)
        {
            return Row
                .Split(new[] {'|'})
                .Chop(1)
                .Select(x => x.Trim())
                .ToList();
        }

        string Name
        {
            get
            {
                return Definition
                    .First()
                    .Split(Colon)[1]
                    .Trim()
                    .CamelCase();    
            } 
        }

        List<Step> Steps
        {
            get
            {
                return Definition.Skip(1)
                    .TakeWhile(IsNotExample)
                    .Select(Step)
                    .Where(Current => Current != null).ToList();
            }
        }

        Step Step(string Line) { return StepParser.StepFrom(Line); }
    }
}