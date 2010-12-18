using System;
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

            return IsArg ? ParseArg :
                IsTable ? ParseTable :
                LastStep = ParseStep;
        }

        Step ParseStep
        {
            get
            {
                var Tokens = Sentence.Split('"');

                return new Step
                {
                    Name = Tokens.Evens().Aggregate((Name, Token) => Name + Token).ToValidIdentifier(),
                    Args = Tokens.Odds().ToList(),
                };
            }
        }

        bool IsTable { get { return Sentence.StartsWith("|") || IsHeader; } }

        bool IsHeader { get { return Sentence.StartsWith("["); } }

        Step ParseTable
        {
            get
            {
                LastStep.AddRow(ParseTableRow());

                if (IsHeader) LastStep.Table.HasHeader = true;

                return null;
            } 
        }

        List<string> ParseTableRow()
        {
            return Sentence
                .Split(new[] {'|'})
                .Chop(1)
                .Select(x => x.Trim())
                .ToList();
        }

        bool ParsingArg;
        bool IsArg 
        { 
            get
            {
                return ParsingArg || IsArgSeparator;
            } 
        }
        bool IsArgSeparator { get { return Sentence.StartsWith("\""); } }

        string Arg = string.Empty;
        Step ParseArg
        {
            get
            {
                if (!ParsingArg) ParsingArg = true;
                else if (!IsArgSeparator) Arg += Sentence + Environment.NewLine;
                else CloseArg();

                return null;
            }
        }

        void CloseArg() 
        {
            LastStep.Args.Add(Arg);
            Arg = string.Empty;
            ParsingArg = false;
        }
    }
}