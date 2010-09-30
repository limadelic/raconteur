using System.Collections.Generic;
using System.Linq;

namespace Raconteur.Parsers
{
    public class StepParserClass : StepParser 
    {
        public Step LastStep { get; set; }

        string Sentence;
        public Step StepFrom(string Sentence)
        {
            this.Sentence = Sentence;

            return IsTable ? ParseStepTable : 
                LastStep = ParseStep;
        }

        Step ParseStep
        {
            get
            {
                var Tokens = Sentence.Split(new[] {'"'});

                return new Step
                {
                    Name = Tokens.Evens().Aggregate((Name, Token) => Name + Token).ToValidIdentifier(),
                    Args = Tokens.Odds().Select(Value).ToList()
                };
            }
        }

        string Value(string Arg)
        {
            if (Arg.IsDateTime()) return @"DateTime.Parse(""" + Arg + @""")";

            if (Arg.IsNumeric() || Arg.IsBoolean() || Arg == "null") 
                return Arg;
             
            return '"' + Arg + '"';
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

                return null;
            } 
        }

        IEnumerable<string> Columns
        {
            get
            {
                return Sentence.Split(new[] {'|'}).Trim(1);
            }
        }

        List<string> Args 
        {
            get
            {
                return Columns.Select(Value).ToList();
            } 
        }
    }
}