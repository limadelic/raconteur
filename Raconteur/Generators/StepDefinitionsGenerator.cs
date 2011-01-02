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

                Result = Result.Replace
                (
                    "namespace " + Namespace,
                    "namespace " + Feature.Namespace
                );

                Result = Result.Replace
                (
                    "public partial class " + ClassName,
                    "public partial class " + Feature.Name
                );

                return Result;
            }
        }

        string Namespace
        {
            get
            {
                return Regex.Match(ExistingStepDefinitions, @"namespace (.+)")
                    .Groups[1].Value.Trim();
            }
        }

        string ClassName
        {
            get
            {
                return Regex.Match(ExistingStepDefinitions, @"public partial class (\w+)")
                    .Groups[1].Value.Trim();
            }
        }
    }
}