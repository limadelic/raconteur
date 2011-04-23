using System;
using System.Linq;
using System.Collections.Generic;
using Raconteur.Compilers;
using Raconteur.Helpers;

namespace Raconteur.Generators
{
    public class StepGeneratorForCompiledStep : CodeGenerator
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

        public StepGeneratorForCompiledStep(Step Step)
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

        IEnumerable<string> FormatArgsOnly
        {
            get
            {
                var Args = Step.ArgDefinitions.Take(Step.Args.Count).ToArray();

                return Step.Args.Select((Arg, i) => 
                    ArgFormatter.Format(Arg, Args[i].ParameterType));
            }
        }

        IEnumerable<string> FormatArgsForTable
        {
            get
            {
                return Step.Table.HasHeader ? 
                    FormatArgsForTableWithHeader : 
                    FormatArgsForSimpleTable;
            }
        }

        IEnumerable<string> FormatArgsForSimpleTable
        {
            get
            {
                var ArgsType = Step.TableItemType();

                return Row.Select(Arg => ArgFormatter.Format(Arg, ArgsType));
            }
        }

        IEnumerable<string> FormatArgsForTableWithHeader
        {
            get
            {
                return Step.HasObjectImplementation
                    ? FormatArgsForObjectTable
                    : FormatArgsForSimpleTableWithHeader;
            }
        }

        private const string ObjectArgTemplate = 
@"        
                new {0}
                {{{1}
                }}";

        private const string FieldInitializerTemplate = 
@"        
                    {0} = {1}";


        IEnumerable<string> FormatArgsForSimpleTableWithHeader
        {
            get
            {
                var Args = Step.ArgDefinitions.Skip(Step.Args.Count).ToArray();

                return Row.Select((Value, i) => 
                    ArgFormatter.Format(Value, Args[i].ParameterType));
            }
        }

        IEnumerable<string> FormatArgsForObjectTable
        {
            get
            {
                var ObjectArg = Step.ObjectArg;

                var FieldsInitialized =
                    Row.Select((Value, i) => FormatArgForObjectInitializer(Value, ObjectArg, Step.Table.Header[i]));

                return new List<string>
                {string.Format(ObjectArgTemplate, ObjectArg.FullName, string.Join(",", FieldsInitialized))};
            }
        }

        string FormatArgForObjectInitializer(string Value, Type ObjectArg, string FieldName)
        {
            return string.Format
            (
                FieldInitializerTemplate,
                FieldName,
                ArgFormatter.Format(Value, ObjectArg.FieldType(FieldName))
            );
        }
    }
}