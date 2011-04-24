using System.Linq;
using System.Collections.Generic;
using Raconteur.Compilers;

namespace Raconteur.Generators
{
    public class StepGeneratorForUnimplementedStep : StepGenerator
    {
        public StepGeneratorForUnimplementedStep(Step Step) : base(Step) {}

        public override IEnumerable<string> FormatArgsOnly
        {
            get { return Step.Args.Select(ArgFormatter.Format); }
        }

        public override IEnumerable<string> FormatArgsForTable
        {
            get { return Row.Select(ArgFormatter.Format); }
        }
    }
}