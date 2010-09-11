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

        public string StepDefinitionsFor(Feature Feature)
        {
            return string.Format(StepDefinitionsClass, 
                Feature.Namespace, 
                Feature.FileName);
        }
    }
}