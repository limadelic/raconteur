using System.Collections.Generic;
using System.Linq;

namespace Raconteur.Generators.Steps
{
    public class StepWithHeaderTableGenerator : StepCodeGenerator
    {
        CodeGenerator CodeGenerator { get; set; }

        public StepWithHeaderTableGenerator(StepGenerator StepGenerator) : base(StepGenerator)
        {
            CodeGenerator = new SimpleStepGenerator(StepGenerator); 
        }

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

            return CodeGenerator.Code;
        }
    }
}