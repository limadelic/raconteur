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
        public string StepDefinitionsFor(Feature Feature, string ExistingStepDefinitions)
        {
            return string.IsNullOrEmpty(ExistingStepDefinitions) ?
                CreateNewStepDefinitions(Feature) :
                RefactorStepDefinitions(Feature, ExistingStepDefinitions);
        }

        private string RefactorStepDefinitions(Feature Feature, string ExistingStepDefinitions)
        {
            var Regex = new Regex(@"public partial class (.+)");
            var ClassName = Regex.Match(ExistingStepDefinitions).Groups[1].Value.Trim();

            return ExistingStepDefinitions.Replace(
                "class " + ClassName, 
                "class " + Feature.Name);
        }

        private string CreateNewStepDefinitions(Feature Feature)
        {
            return string.Format(StepDefinitionsClass, 
                Feature.Namespace, 
                Feature.Name);
        }
    }
}