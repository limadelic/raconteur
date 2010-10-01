using System.Collections.Generic;
using System.Linq;

namespace Raconteur.Parsers
{
    public class StepParserClass : StepParser 
    {
        public Step LastStep { get; set; }
        public List<Step> Steps { get; set; }
        public List<List<string>> ArgColMap { get; set; }

        public StepParserClass()
        {
            Steps = new List<Step>();
            ArgColMap = new List<List<string>>();
        }

        string Sentence;
        public Step StepFrom(string Sentence)
        {
            this.Sentence = Sentence;

            if (IsTable) return ParseStepTable;

            var Step = LastStep = ParseStep;
            Steps.Add(Step);
            return Step;
        }

        Step ParseStep
        {
            get
            {
                var sentence = Sentence;
                var IsOutline = sentence.Contains('<') && sentence.Contains('>');

                if (IsOutline)
                {
                    Steps.ForEach(Step => Step.Skip = true);
                    sentence = sentence.Replace('<', '"').Replace('>', '"');
                }

                var Tokens = sentence.Split(new[] {'"'});

                return new Step
                {
                    Name = Tokens.Evens().Aggregate((Name, Token) => Name + Token).ToValidIdentifier(),
                    Args = Tokens.Odds().Select(Arg).ToList(),
                    Skip = IsOutline
                };
            }
        }

        string Arg(string Value)
        {
            if (Value.IsDateTime()) return @"DateTime.Parse(""" + Value + @""")";

            if (Value.IsNumeric() || Value.IsBoolean() || Value == "null") 
                return Value;
             
            return '"' + Value + '"';
        }

        bool IsTable 
        { 
            get
            {
                var isTable = Sentence.StartsWith("|");

                if (!isTable) StopParsingTable();

                return isTable;
            }
        }

        bool ParsedHeader;

        void StopParsingTable() { ParsedHeader = false; }

        Step ParseStepTable
        {
            get
            {
                return !ParsedHeader ? ParseStepTableHeader :
                    ParseStepTableRow;
            } 
        }

        Step ParseStepTableRow
        {
            get
            {
                return new Step
                {
                    Name = LastStep.Name,
                    Args = Args
                };
            }
        }

        Step ParseStepTableHeader
        {
            get
            {
                ParsedHeader = true;
                LastStep.Skip = true;

                SetUpArgColMap();

                return null;
            } 
        }

        void SetUpArgColMap()
        {
            foreach (var Column in Columns)
                ArgColMap.Add(ArgsMatching(Column));
        }

        List<string> ArgsMatching(string Column)
        {
            return 
            (
                from Step in Steps 
                from arg in Step.Args 
                where Arg(Column).Equals(arg) 
                select arg
            ).ToList();
        }

        IEnumerable<string> Columns
        {
            get
            {
                return Sentence.Split(new[] {'|'}).Chop(1).
                    Select(x => x.Trim());
            }
        }

        List<string> Args 
        {
            get
            {
                return LastStep.Args.Concat(Columns.Select(Arg)).ToList();
            } 
        }
    }
}