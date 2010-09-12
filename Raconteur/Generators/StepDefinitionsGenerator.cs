using System.Collections.Generic;
using System.Linq;

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
{2}
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

        Feature Feature;
        public string StepDefinitionsFor(Feature Feature)
        {
            this.Feature = Feature;

            return string.Format(StepDefinitionsClass, 
                Feature.Namespace, 
                Feature.FileName,
                StepDeclarations);
        }

        public string DeclareStep(string Step) 
        { 
            return string.Format(StepDeclaration, Step);
        }

        string StepDeclarations 
        {
            get
            {
                return Feature.Scenarios.Aggregate(new List<string>() as IEnumerable<string>,
                    (Steps, Scenario) => Steps.Union(Scenario.Steps))
                    .Aggregate("", (Steps, Step) => Steps + DeclareStep(Step));
            } 
        }
    }
}