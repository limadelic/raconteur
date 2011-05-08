using System;
using System.Collections.Generic;
using System.Linq;

namespace Raconteur.Compilers
{
    public static class TableArgsFormatters
    {
        public static IEnumerable<string> FormatArgs(this Step Step, IEnumerable<string> Row = null)
        {
            return ArgsFormatter[Step.Type](Step, Row);
        }

        static readonly Dictionary<StepType, Func<Step, IEnumerable<string>, IEnumerable<string>>>
            ArgsFormatter = new Dictionary<StepType, Func<Step, IEnumerable<string>, IEnumerable<string>>>
            {
                { StepType.Table, (Step, Row) => FormatTableArgs(Step, Row) },    
                { StepType.HeaderTable, (Step, Row) => FormatHeaderTableArgs(Step, Row) },    
                { StepType.ObjectTable, (Step, Row) => FormatObjectTableArgs(Step, Row) },    
            };

        static IEnumerable<string> FormatTableArgs(Step Step, IEnumerable<string> Row)
        {
            return Row.Select(Arg => ArgFormatter.Format(Arg, Step.TableItemType()));
        }

        static IEnumerable<string> FormatHeaderTableArgs(Step Step, IEnumerable<string> Row)
        {
            var Args = Step.ArgDefinitions.Skip(Step.Args.Count).ToArray();

            return Row.Select((Value, i) => 
                ArgFormatter.Format(Value, Args[i].ParameterType));            
        }

        private const string ObjectArgTemplate = 
@"        
                new {0}
                {{{1}
                }}";

        private const string FieldInitializerTemplate = 
@"        
                    {0} = {1}";

        static IEnumerable<string> FormatObjectTableArgs(Step Step, IEnumerable<string> Row)
        {
            var FieldsInitialized = Row.Select((Value, i) => 
                FormatArgForObjectInitializer
                (
                    Value, 
                    Step.ObjectArg, 
                    Step.ObjectArg.FieldName(Step.Table.Header[i])
                ));

            return new List<string>
            {
                string.Format
                (
                    ObjectArgTemplate, 
                    Step.ObjectArg.FullName, 
                    string.Join(",", FieldsInitialized)
                )
            };
        }

        static string FormatArgForObjectInitializer(string Value, Type ObjectArg, string FieldName)
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