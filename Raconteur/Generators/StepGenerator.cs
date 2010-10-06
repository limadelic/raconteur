using System.Linq;
using System.Collections.Generic;

namespace Raconteur.Generators
{
    class StepGenerator : CodeGenerator
    {
        private const string StepExecution = 
@"        
            {0}({1});";

        readonly Step Step;

        public StepGenerator(Step Step) 
        {
            this.Step = Step;
        }

        public string Code
        {
            get
            {
                return Step.HasTable ?
                    CodeForStepWithTable :
                    CodeFor(Step);
            }
        }

        string CodeForStepWithTable
        {
            get
            {
                return Step.Table.Rows.Skip(1)
                    .Aggregate(string.Empty, (Steps, Row) => 
                        Steps + ExecuteStepRow(Row));
            }
        }

        string ExecuteStepRow(List<string> Row) 
        { 
            return CodeFor
            (
                new Step
                {
                    Name = Step.Name,
                    Args = Step.Args.Concat(Row).ToList()
                }
            );
        }

        string CodeFor(Step Step) 
        {
            var ArgsValues = Step.Args.Select(ArgFormatter.ValueOf);

            var Args = string.Join(", ", ArgsValues);

            return string.Format(StepExecution, Step.Name, Args);
        }
    }
}