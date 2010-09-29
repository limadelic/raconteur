using System;
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

        protected Step ParseStepTable
        {
            get
            {
                if (!ParsedHeader)
                {
                    ParsedHeader = true;
                    return null;
                }

                return new Step {Name = LastStep.Name};
            } 
        }
    }
}