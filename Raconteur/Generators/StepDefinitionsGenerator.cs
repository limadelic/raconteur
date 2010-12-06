using System.Text.RegularExpressions;

namespace Raconteur.Generators
{
    public class StepDefinitionsGenerator : CodeGenerator
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

        readonly Feature Feature;
        readonly string ExistingStepDefinitions;

        public string Code
        {
            get
            {
                return ExistingStepDefinitions.IsEmpty() ?
                    NewStepDefinitions :
                    RefactoredStepDefinitions;
            }
        }

        internal StepDefinitionsGenerator(Feature Feature, string ExistingStepDefinitions)
        {
            this.Feature = Feature;
            this.ExistingStepDefinitions = ExistingStepDefinitions;
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