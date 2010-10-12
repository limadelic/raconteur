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

            if (IsTable)
            {
                AddRowToStep();
                return null;
            }

            return LastStep = ParseStep;
        }

        void AddRowToStep()
        {
            LastStep.AddRow(ParseTableRow());
        }

        List<string> ParseTableRow()
        {
            return Sentence
                .Split(new[] {'|'})
                .Chop(1)
                .Select(x => x.Trim())
                .ToList();
        }

        bool IsTable
        {
            get
            {
                return Sentence.StartsWith("|") || Sentence.StartsWith("[");
            }
        }

        Step ParseStep
        {
            get
            {
                var Tokens = Sentence.Split(new[] {'"'});

                return new Step
                {
                    Name = Tokens.Evens().Aggregate((Name, Token) => Name + Token).ToValidIdentifier(),
                    Args = Tokens.Odds().ToList(),
                };
            }
        }
    }
}