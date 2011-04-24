using System.Linq;
using System.Collections.Generic;
using Raconteur.Compilers;

namespace Raconteur.Generators
{
    public class StepGeneratorForNewStep : StepGenerator
    {
        public StepGeneratorForNewStep(Step Step) : base(Step) {}

        protected override IEnumerable<string> FormatArgsOnly
        {
            get { return Step.Args.Select(ArgFormatter.Format); }
        }

        protected override IEnumerable<string> FormatArgsForTable
        {
            get { return Row.Select(ArgFormatter.Format); }
        }
    }
}