using System.Collections.Generic;
using System.Linq;

namespace Raconteur.Parsers
{
    public class ScenarioParserClass : ScenarioParser
    {
        public StepParser StepParser { get; set; }

        public List<string> Definition;

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

        bool InsideArg;

        protected bool IsNotExample(string Line)
        {
            if (Line == "\"") InsideArg = !InsideArg;

            return InsideArg || !Line.StartsWith(Settings.Language.Examples);
        }

        List<string> ParseTableRow(string Row)
        {
            return Row
                .Split('|')
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
                    .Split(A.Colon, 2)[1]
                    .Trim()
                    .CamelCase()
                    .ToValidIdentifier();    
            } 
        }

        public List<Step> Steps
        {
            get
            {
                return Definition.Skip(1)
                    .TakeWhile(IsNotExample)
                    .Select(Step)
                    .Where(Current => Current != null).ToList();
            }
        }

        Step Step(string Line)
        {
            var StepFrom = StepParser.StepFrom(Line);
            return StepFrom;
        }
    }
}