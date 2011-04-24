using System.Collections.Generic;
using System.Linq;

namespace Raconteur.Generators.Steps
{
    public class SimpleStepGenerator : StepCodeGenerator
    {
        public SimpleStepGenerator(StepGenerator StepGenerator) : base(StepGenerator) {}

        public override string Code
        {
            get
            {
                return string.Format
                (
                    StepGenerator.StepTemplate, 
                    Step.Call, 
                    CodeForArgs
                );
            }
        }

        string CodeForArgs { get { return string.Join(", ", FormatArgs); } }

        IEnumerable<string> FormatArgs
        {
            get
            {
                var FormattedArgs = StepGenerator.FormatArgsOnly;

                return Step.HasTable ? 
                    FormattedArgs.Concat(StepGenerator.FormatArgsForTable) : 
                    FormattedArgs;
            }
        }
    }
}