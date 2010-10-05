using System;
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


        //        public List<Scenario> ScenariosFrom(string Content)
//        {
//            this.Content = Content;
//            Scenarios = new List<Scenario>();
//
//            ScenarioDefinitions.ForEach(Parse);
//
//            return Scenarios;
//        }

//        IEnumerable<string> Lines { get { return 
//            
//            Content
//            .Split(NewLine)
//            .SkipWhile(IsNotScenarioDeclaration)
//            .Where(Line => !string.IsNullOrWhiteSpace(Line))
//            .Select(Line => Line.Trim());    
//        }}
//
//        bool IsScenarioDeclaration(string Line)
//        {
//            return Line.TrimStart().StartsWith(ScenarioDeclaration);
//        }
//
//        bool IsNotScenarioDeclaration(string Line)
//        {
//            return !IsScenarioDeclaration(Line);
//        }
//
//        List<List<string>> ScenarioDefinitions
//        {
//            get
//            {
//                var Results = new List<List<string>>();
//
//                foreach (var Line in Lines)
//                {
//                    if (IsScenarioDeclaration(Line))
//                        Results.Add(new List<string>());
//                    
//                    Results.Last().Add(Line);
//                }
//
//                return Results;
//            }
//        }

        /*
        void Parse(List<string> ScenarioDefinition)
        {
            this.ScenarioDefinition = ScenarioDefinition;

            if (IsScenarioOutline) 
                ParseScenarioOutline();
            else ParseScenario();
        }
*/

/*
        bool IsScenarioOutline { get { return 
            
            ScenarioDefinition.First()
            .StartsWith(ScenarioOutlineDeclaration)
        ;}}

        void ParseScenario()
        {
            AddScenario
            (
                Name, 
                StepsFrom(ScenarioDefinition.Skip(1))
            );
        }
*/

        //        void ParseScenarioOutline()
//        {
//            ParseLines();
//
//            var Outline = new Scenario
//            {
//                Name = Name,
//                Steps = OutlineSteps
//            };
//
//            var i = 1;
//            foreach (var Line in ExampleLines)
//                AddScenario(Name + i++, null);
//        }
//
//        string NameLine;
//        List<string> OutlineLines;
//        string ExampleHeaderLine;
//        List<string> ExampleLines;

//        void ParseLines()
//        {
//            NameLine = ScenarioDefinition.First();
//            OutlineLines = ScenarioDefinition.Skip(1)
//                .TakeWhile(IsNotExample).ToList();
//            ExampleHeaderLine = ScenarioDefinition.Skip(OutlineLines.Count + 1).First();
//            ExampleLines = ScenarioDefinition.Skip(OutlineLines.Count + 2).ToList();
//        }

//        bool IsNotExample(string Line) 
//        { 
//            return !Line.StartsWith(ExamplesDeclaration);
//        }
//
//        List<Step> OutlineSteps
//        {
//            get { return OutlineLines.Select(OutlineStep).ToList(); }
//        }
//
//        Step OutlineStep(string Line)
//        {
//            return StepParser.OutlineStepFrom(Line);
//        }
    }
}