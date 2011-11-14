using System.Linq;
using System.Collections.Generic;
using Raconteur.Compilers;

namespace Raconteur.Generators.Steps
{
    public class StepGeneratorForImplementedStep : StepGenerator
    {

        public StepGeneratorForImplementedStep(Step Step) : base(Step) {}

        public override IEnumerable<string> FormatArgsOnly
        {
            get
            {
                var Args = Step.ArgDefinitions.Take(Step.Args.Count).ToArray();

                return Step.Args.Select((Arg, i) => 
                    ArgFormatter.Format(Arg, Args[i].ParameterType));
            }
        }

        public override IEnumerable<string> FormatArgsForTable
        {
            get { return Step.FormatArgs(Row); }
        }
    }
}