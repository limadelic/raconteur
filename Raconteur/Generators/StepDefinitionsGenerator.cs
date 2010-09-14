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

        const string StepDeclaration = 
@"        
        void {0}()
        {{ 
            throw new System.NotImplementedException(""Pending Step {0}"");
        }}
";

        public string StepDefinitionsFor(Feature Feature)
        {
            return string.Format(StepDefinitionsClass, 
                Feature.Namespace, 
                Feature.Name);
        }

        public string DeclareStep(string Step) 
        { 
            return string.Format(StepDeclaration, Step);
        }
    }
}