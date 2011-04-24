using System.Collections.Generic;
using System.Linq;

namespace Raconteur.Generators
{
    public class StepWithHeaderTableGenerator : StepCodeGenerator
    {
        public StepWithHeaderTableGenerator(StepGenerator StepGenerator) : base(StepGenerator) {}

        public override string Code
        {
            get 
            {
                return Step.Table.Rows.Skip(1)
                    .Aggregate(string.Empty, (Steps, Row) =>
                        Steps + ExecuteStepRow(Row));
            }
        }

        string ExecuteStepRow(IEnumerable<string> Row) 
        {
            StepGenerator.Row = Row;

            return StepGenerator.CodeForStep;
        }
    }
}