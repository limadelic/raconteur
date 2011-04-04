using System.Linq;
using System.Collections.Generic;
using Raconteur.Compilers;
using Raconteur.Helpers;

namespace Raconteur.Generators
{
    public class StepGenerator : CodeGenerator
    {
        private const string StepExecution = 
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
                    ArgsCode = ArgsCode.Insert(0, "\r\n" + CodeForArgsOf(Step) + ",\r\n");

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

        string ExecuteStepRow(IEnumerable<string> Row) 
        { 
            return CodeFor
            (
                new Step
                {
                    Name = Step.Name,
                    Args = Step.Args.Concat(Row).ToList(),
                    Implementation = Step.Implementation
                }
            );
        }

        string ArgsFrom(IEnumerable<string> Row) 
        { 
            var ArgsValues = (Step.IsImplemented ?
                FormatArgsForTable(Row) :
                Row.Select(ArgFormatter.Format)).ToList();

            return string.Join(", ", ArgsValues);
        }

        string CodeFor(Step Step)
        {
            return string.Format
            (
                StepExecution, 
                Step.Call, 
                CodeForArgsOf(Step)
            );
        }

        string CodeForArgsOf(Step Step)
        {
            return string.Join(", ", 
                Step.IsImplemented ?
                    FormatArgs(Step) :
                    Step.Args.Select(ArgFormatter.Format));
        }

        IEnumerable<string> FormatArgs(Step Step)
        {
            return Step.Args.Select((Arg, i) => ArgFormatter.Format
            (
                Arg, 
                Step.Implementation.GetParameters()[i].ParameterType
            ));
        }

        IEnumerable<string> FormatArgsForTable(IEnumerable<string> Row) 
        {
            return Step.Table.HasHeader ? 
                FormatArgsAsDeclaredForTableWithHeader(Row) :
                FormatArgsAsDeclaredForSimpleTable(Row);
        }

        IEnumerable<string> FormatArgsAsDeclaredForTableWithHeader(IEnumerable<string> Row)
        {
            var Args = Step.Implementation.GetParameters()
                .Skip(Step.Args.Count).ToArray();

            return Row.Select((Value, i) => 
                ArgFormatter.Format(Value, Args[i].ParameterType));
        }

        IEnumerable<string> FormatArgsAsDeclaredForSimpleTable(IEnumerable<string> Row) 
        {
            var ArgsType = Step.TableItemType();
            
            return Row.Select(Arg => ArgFormatter.Format(Arg, ArgsType));
        }
    }
}