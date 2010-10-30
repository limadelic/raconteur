using System.Text.RegularExpressions;

namespace Raconteur.Generators
{
    public class StepDefinitionsGenerator
    {
        const string StepDefinitionsClass = 

@"
namespace {0} 
{{
    public partial class {1} 
    {{
    }}
}}
";
        Feature Feature;
        string ExistingStepDefinitions;

        public string StepDefinitionsFor(Feature Feature, string ExistingStepDefinitions)
        {
            this.Feature = Feature;
            this.ExistingStepDefinitions = ExistingStepDefinitions;

            return ExistingStepDefinitions.IsEmpty() ?
                NewStepDefinitions :
                RefactoredStepDefinitions;
        }

        string NewStepDefinitions
        {
            get
            {
                return string.Format
                (
                    StepDefinitionsClass, 
                    Feature.Namespace, 
                    Feature.Name
                );
            }
        }

        string RefactoredStepDefinitions
        {
            get
            {
                var Result = ExistingStepDefinitions;

                Result = Rename("namespace ", Feature.Namespace, Result);
                Result = Rename("public partial class ", Feature.Name, Result);

                return Result;
            }
        }

        string Rename(string Preffix, string NewName, string Text) 
        {
            var Regex = new Regex(Preffix + @"(\w+)");
            var OldName = Regex.Match(Text).Groups[1].Value.Trim();

            return Text.Replace(Preffix + OldName, Preffix + NewName);
        }
    }
}