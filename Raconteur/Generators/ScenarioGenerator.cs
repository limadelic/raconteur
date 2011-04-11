using System.Collections.Generic;
using System.Linq;
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
                
                SetupOutline();

                foreach (var Example in Scenario.Examples)
                for (var Row = 0; Row < Example.Count; Row++)
                {
                    Scenario.Name = Scenario.OriginalName + "_" + Example.Name + (Row + 1);

                    for (var Col = 0; Col < Example.Header.Count; Col++) 
                        ReplaceExampleInStepsArgs(Example.Header[Col], Example[Row, Col]);

                    Result += ScenarioCode;
                }

                return Result;
            }
        }

        Dictionary<string, Dictionary<Step, List<int>>> ArgOutlines;

        void SetupOutline()
        {
            Scenario.OriginalName = Scenario.Name;

            var Names = Scenario.Examples[0].Header;

            ArgOutlines = new Dictionary<string, Dictionary<Step, List<int>>>();

            foreach (var Step in Scenario.Steps)
            for (var i = 0; i < Step.Args.Count; i++)
            {
                var Arg = Step.Args[i];    
                
                if (!Names.Contains(Arg)) continue;
                
                if (!ArgOutlines.ContainsKey(Arg)) ArgOutlines[Arg] = new Dictionary<Step, List<int>>();

                if (!ArgOutlines[Arg].ContainsKey(Step)) ArgOutlines[Arg][Step] = new List<int>();

                ArgOutlines[Arg][Step].Add(i);
            }
        }

        void ReplaceExampleInStepsArgs(string Name, string Value)
        {
            if (!ArgOutlines.ContainsKey(Name)) return;

            foreach (var Step in ArgOutlines[Name].Keys)
            foreach (var Arg in ArgOutlines[Name][Step])
                Step.Args[Arg] = Value;
        }
    }
}