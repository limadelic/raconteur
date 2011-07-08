using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Raconteur.Helpers;

namespace Raconteur.Parsers
{
    public interface ScenarioParser
    {
        Scenario ScenarioFrom(List<Location> Definition, Location Location=null);
    }

    public class ScenarioParserClass : ScenarioParser
    {
        public StepParser StepParser { get; set; }
        public string Content { get; set; }

        public List<Location> Definition;

        IEnumerable<string> DefinitionContent
        {
            get { return Definition.Select(x => x.Content); }
        }

        public Scenario ScenarioFrom(List<Location> Definition, Location Location=null)
        {
            this.Definition = Definition;

            return new Scenario
            {
                Tags = Tags,
                Name = Name,
                Steps = Steps,
                Examples = Examples,
                Location = Location
            };
        }

        public List<string> Tags
        {
            get
            {
                return DefinitionContent
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
                return DefinitionContent
                    .SkipWhile(IsNotDeclaration)
                    .First()
                    .YamlIdentifier();    
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

        Step Step(Location Location)
        {
            return StepParser.StepFrom(Location);
        }

        bool InsideArg;

        bool IsNotDeclaration(Location Location)
        {
            return IsNotDeclaration(Location.Content);
        }

        bool IsNotDeclaration(string Line)
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

        #region examples
		        
        protected List<Table> Examples
        {
            get
            {
                var examples = new List<Table>();

                foreach (var Line in DefinitionContent.SkipWhile(IsNotExample))
                    if (IsExample(Line)) examples.Add
                    (
                        new Table { Name = Line.YamlIdentifier()}
                    );
                    else examples.Last().Add(ParseTableRow(Line));

                return examples;
            }
        }

        protected bool IsExample(string Line)
        {
            return !IsNotExample(Line);
        }

        protected bool IsNotExample(Location Location)
        {
            return IsNotExample(Location.Content);
        }

        protected bool IsNotExample(string Line)
        {
            if (Line == "\"") InsideArg = !InsideArg;

            return InsideArg || !Line.StartsWith(Settings.Language.Examples);
        }

	    #endregion    
    }
}