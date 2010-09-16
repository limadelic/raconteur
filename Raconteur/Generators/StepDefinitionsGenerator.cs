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

        string RefactorStepDefinitions(Feature Feature, string ExistingStepDefinitions)
        {
            const string ClassDeclaration = "public partial class ";

            var Regex = new Regex(ClassDeclaration + @"(.+)[{]");
            var ClassName = Regex.Match(ExistingStepDefinitions).Groups[1].Value.Trim();

            return ExistingStepDefinitions.Replace(
                ClassDeclaration + ClassName, 
                ClassDeclaration + Feature.Name);
        }

        string CreateNewStepDefinitions(Feature Feature)
        {
            return string.Format(StepDefinitionsClass, 
                Feature.Namespace, 
                Feature.Name);
        }
    }
}