using System.Collections.Generic;
using System.Linq;
using Raconteur.Helpers;

namespace Raconteur.Generators
{
    public abstract class StepGenerator : CodeGenerator
    {
        protected abstract IEnumerable<string> FormatArgsOnly { get; }
        protected abstract IEnumerable<string> FormatArgsForTable { get; }

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

        protected readonly Step Step;

        protected StepGenerator(Step Step)
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

        protected IEnumerable<string> Row;

        string ExecuteStepRow(IEnumerable<string> Row)
        {
            this.Row = Row;
            
            return CodeForStep;
        }

        string ArgsFrom(IEnumerable<string> Row)
        {
            this.Row = Row;

            return string.Join(", ", FormatArgsForTable);
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
    }
}