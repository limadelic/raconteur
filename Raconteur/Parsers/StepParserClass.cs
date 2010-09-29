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

            return IsTable ? ParseStepTable : ParseStep;
        }

        Step ParseStep
        {
            get
            {
                var Tokens = Sentence.Split(new[] {'"'});

                return new Step
                {
                    Name = Tokens.Evens().Aggregate((Name, Token) => Name + Token).ToValidIdentifier(),
                    Args = Tokens.Odds().Select(x => '"' + x + '"').ToList()
                };
            }
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
                CopyColumnsToLastStepArgs();

                return new Step
                {
                    Name = LastStep.Name,
                    Args = LastStep.Args.ToList()
                };
            }
        }

        Step ParseStepTableHeader
        {
            get
            {
                SetUpInLastStepOneArgPerColumn();

                ParsedHeader = true;

                return null;
            } 
        }

        List<string> Columns
        {
            get
            {
                return Sentence.Split(new[] {'|'}).Trim(1).ToList();
            }
        }

        void SetUpInLastStepOneArgPerColumn()
        {
            LastStep.Args = new List<string>();
            Columns.ForEach(LastStep.Args.Add);
        }

        void CopyColumnsToLastStepArgs() 
        {
            var i = 0;
            foreach (var Column in Columns)
            {
                if (!string.IsNullOrWhiteSpace(Column)) 
                    LastStep.Args[i] = Column;
                i++;
            }
        }
    }
}