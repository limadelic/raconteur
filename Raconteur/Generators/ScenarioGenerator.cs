using System;
using System.Collections.Generic;
using System.Linq;
using Raconteur.Compilers;
using Raconteur.Helpers;

namespace Raconteur.Generators
{
    public class ScenarioGenerator : CodeGenerator
    {
        private const string ScenarioDeclaration = 
@"        
        {0}{1}{2}
        public void {3}()
        {{ {4}
        }}
";

        private const string TagDeclaration = 
@"        
        {0}";

        readonly Scenario Scenario;

        public ScenarioGenerator(Scenario Scenario)
        {
            this.Scenario = Scenario;
        }

        public string Code
        {
            get
            {
                return Scenario.IsOutline ?
                    OutlineScenarioCode :
                    ScenarioCode;
            }
        }

        string ScenarioCode
        {
            get
            {
                var StepCode = Scenario.Steps.Aggregate(string.Empty, 
                    (Steps, Step) => Steps + CodeFor(Step));

                return string.Format
                (
                    ScenarioDeclaration, 
                    Settings.XUnit.MethodAttr,
                    Tags, 
                    Ignore,
                    Scenario.Name, 
                    StepCode
                );
            }
        }

        string Tags
        {
            get
            {
                return Scenario.Tags.Where(IsNotIgnored).Aggregate(string.Empty, (Results, Tag) =>
                    Results + string.Format(TagDeclaration, 
                        string.Format(Settings.XUnit.Category, Tag)));
            }
        }

        string Ignore
        {
            get
            {
                var IgnoreTag = Scenario.Tags.FirstOrDefault(IsIgnored);

                if (IgnoreTag == null) return string.Empty;

                var Reason = IgnoreTag.Remove(0, 6);

                return string.Format
                (
                    TagDeclaration, 
                    Reason.IsEmpty() ? 
                        Settings.XUnit.Ignore :
                        string.Format(Settings.XUnit.IgnoreWithReason, Reason.Trim())
                );
            }
        }

        bool IsIgnored(string Tag) { return Tag.ToLower().StartsWith("ignore"); }
        
        bool IsNotIgnored(string Tag) { return !IsIgnored(Tag); }

        string CodeFor(Step Step) { return new StepGenerator(Step).Code; }

        string OutlineScenarioCode
        {
            get
            {
                var Result = string.Empty;
                
                BackupScenario();
                SetupOutline();

                foreach (var Example in Scenario.Examples)
                for (var Row = 0; Row < Example.Count; Row++)
                {
                    Scenario.Name = Scenario.OriginalName + "_" + Example.Name + (Row + 1);

                    for (var Col = 0; Col < Example.Header.Count; Col++) 
                        ReplaceExampleInStepsArgs(Example.Header[Col], Example[Row, Col]);

                    Result += ScenarioCode;

                    RestoreScenario();
                }

                return Result;
            }
        }

        Dictionary<string, Dictionary<Step, List<int>>> StepArgMap;

        void SetupOutline()
        {
            StepArgMap = new Dictionary<string, Dictionary<Step, List<int>>>();

            foreach (var ColHeader in Scenario.Examples[0].Header)
            foreach (var Step in Scenario.Steps)
            for (var i = 0; i < Step.Args.Count; i++)
            {
                var Arg = Step.Args[i];

                var MatchesHeader = ColHeader == Arg;
                var ContainsArg = Arg.IsMultiline() && Arg.Contains(ColHeader.Quoted());
                
                if (!MatchesHeader && !ContainsArg) continue;
                
                EnsureStepArgMapEntry(Step, ColHeader);

                StepArgMap[ColHeader][Step].Add(i);
            }
        }

        void EnsureStepArgMapEntry(Step Step, string ColHeader)
        {
            if (!StepArgMap.ContainsKey(ColHeader)) 
                StepArgMap[ColHeader] = new Dictionary<Step, List<int>>();

            if (!StepArgMap[ColHeader].ContainsKey(Step)) 
                StepArgMap[ColHeader][Step] = new List<int>();
        }

        void RestoreScenario()
        {
            Scenario.Name = Scenario.OriginalName;
            Scenario.Steps.ForEach(x => x.Args = x.OriginalArgs.ToList());
        }

        void BackupScenario()
        {
            Scenario.OriginalName = Scenario.Name;
            Scenario.Steps.ForEach(x => x.OriginalArgs = x.Args.ToList());
        }

        void ReplaceExampleInStepsArgs(string ColHeader, string Value)
        {
            if (!StepArgMap.ContainsKey(ColHeader)) return;

            foreach (var Step in StepArgMap[ColHeader].Keys)
            foreach (var Arg in StepArgMap[ColHeader][Step])
                Step.Args[Arg] = !Step.OriginalArgs[Arg].IsMultiline() ? Value :
                    Step.Args[Arg].Replace(ColHeader.Quoted().Quoted(), Value);
        }
    }
}