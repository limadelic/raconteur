using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
                Tags = Tags,
                Name = Name,
                Steps = Steps,
                Examples = Examples
            };
        }

        public List<string> Tags
        {
            get
            {
                return Definition
                    .TakeWhile(IsNotDeclaration)
                    .SelectMany(TagsInLine)
                    .Distinct()
                    .ToList();
            }
        }

        protected IEnumerable<string> TagsInLine(string Line) 
        {
            return 
                from Tag in Regex.Split(Line, "@")
                where !Tag.IsEmpty()
                select Tag.Trim();
        }

        string Name
        {
            get
            {
                return Definition
                    .SkipWhile(IsNotDeclaration)
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
                return Definition
                    .SkipWhile(IsNotDeclaration)
                    .Skip(1)
                    .TakeWhile(IsNotExample)
                    .Select(Step)
                    .WhereIsNotNull()
                    .ToList();
            }
        }

        Step Step(string Line)
        {
            var StepFrom = StepParser.StepFrom(Line);
            return StepFrom;
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

        protected bool IsNotDeclaration(string Line)
        {
            return !Line.StartsWith(Settings.Language.Scenario);
        }

        List<string> ParseTableRow(string Row)
        {
            return Row
                .Split('|')
                .Chop(1)
                .Select(x => x.Trim())
                .ToList();
        }
    }
}