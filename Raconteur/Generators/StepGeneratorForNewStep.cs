using System.Linq;
using System.Collections.Generic;
using Raconteur.Compilers;
using Raconteur.Helpers;

namespace Raconteur.Generators
{
    public class StepGeneratorForNewStep : CodeGenerator
    {
        private const string StepTemplate = 
@"        
            {0}({1});";

        private const string MultilineStepExecution = 
@"        
            {0}
            ({1}
            );";

        private const string StepRowExecution = 
@"        
                new[] {{{0}}},";

        readonly Step Step;

        public StepGeneratorForNewStep(Step Step)
        {
            this.Step = Step;
        }

        public string Code
        {
            get
            {
                return Step.HasTable ? 
                    CodeForStepWithTable : 
                    CodeForStep;
            }
        }

        string CodeForStepWithTable
        {
            get
            {
                return Step.Table.HasHeader ? 
                    CodeForStepTableWithHeader :
                    CodeForStepWithSimpleTable;
            }
        }

        string CodeForStepWithSimpleTable
        {
            get
            {
                var Table = Step.Table.Rows
                    .Aggregate("", (Steps, Row) => Steps + 
                        string.Format(StepRowExecution, ArgsFrom(Row)));

                var ArgsCode = Table.RemoveTail(1);
                
                if (Step.HasArgs) 
                    ArgsCode = ArgsCode.Insert(0, "\r\n" + CodeForArgsOnly + ",\r\n");

                return string.Format
                (
                    MultilineStepExecution, 
                    Step.Name,
                    ArgsCode
                );
            }
        }

        string CodeForStepTableWithHeader
        {
            get
            {
                return Step.Table.Rows.Skip(1)
                    .Aggregate(string.Empty, (Steps, Row) => 
                        Steps + ExecuteStepRow(Row));
            }
        }

        IEnumerable<string> Row;

        string ExecuteStepRow(IEnumerable<string> Row)
        {
            this.Row = Row;
            
            return CodeForStep;
        }

        string ArgsFrom(IEnumerable<string> Row)
        {
            this.Row = Row;

            return string.Join(", ", Row.Select(ArgFormatter.Format));
        }

        string CodeForStep
        {
            get
            {
                return string.Format
                (
                    StepTemplate, 
                    Step.Call, 
                    CodeForArgs
                );
            }
        }

        string CodeForArgsOnly
        {
            get
            {
                return string.Join(", ", FormatArgsOnly);
            }
        }

        string CodeForArgs
        {
            get
            {
                return string.Join(", ", FormatArgs);
            }
        }

        IEnumerable<string> FormatArgs
        {
            get
            {
                var FormattedArgs = FormatArgsOnly;

                return Step.HasTable ? 
                    FormattedArgs.Concat(FormatArgsForTable) : 
                    FormattedArgs;
            }
        }

        IEnumerable<string> FormatArgsOnly
        {
            get
            {
                return Step.Args.Select(ArgFormatter.Format);
            }
        }

        IEnumerable<string> FormatArgsForTable
        {
            get
            {
                return Row.Select(ArgFormatter.Format);
            }
        }
    }
}