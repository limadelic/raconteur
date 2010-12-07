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
                return new Regex(@"namespace (.+)")
                    .Match(ExistingStepDefinitions)
                    .Groups[1].Value.Trim();
            }
        }

        string ClassName
        {
            get
            {
                return new Regex(@"public partial class (\w+)")
                    .Match(ExistingStepDefinitions)
                    .Groups[1].Value.Trim();
            }
        }
    }
}