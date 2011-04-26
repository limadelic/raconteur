using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Raconteur.Compilers;

namespace Raconteur
{
    public class Step
    {
        public Feature Feature { get; set; }

        public string Name { get; set; }
        public List<string> Args { get; set; }
        public List<string> OriginalArgs { get; set; }

        public bool HasArgs { get { return Args.Count > 0; } }

        public int ArgsCount
        {
            get
            {
                return Args.Count/* +
                    (
                        !HasTable ? 0 :
                        !Table.HasHeader ? 1 :
                        Table.Header.Count
                    )*/;
            }
        }

        public MethodInfo Implementation { get; set; }
        
        public bool IsImplemented { get { return Implementation != null; } }

        public ParameterInfo[] ArgDefinitions { get { return Implementation.GetParameters(); } }

        public Step()
        {
            ArgsMatcher = new SimpleArgsMatcher(this);
            Args = new List<string>();
        }

        public string Call
        {
            get
            {
                if (!IsImplemented || Feature == null || !Feature.HasStepDefinitions) 
                    return Name;

                return  IsImplementedInFeatureSteps ? Name :
                    StepDefinitions.Name + "." + Name;
            } 
        }

        bool IsImplementedInFeatureSteps
        {
            get
            {
                return StepDefinitions == null || 
                    StepDefinitions == Feature.DefaultStepDefinitions;
            }
        }

        Type stepDefinitions;
        Type StepDefinitions
        {
            get
            {
                return stepDefinitions ?? 
                    (stepDefinitions = Feature.StepDefinitions.Where(ImplementsStep).FirstOrDefault());
            } 
        }
        
        bool ImplementsStep(Type StepDefinition)
        {
            return StepDefinition == Implementation.DeclaringType
                || StepDefinition.IsSubclassOf(Implementation.DeclaringType);
        }

        public bool HasTable { get { return Table != null; } }

        Table table;
        public Table Table
        {
            get { return table; } 
            set
            {
                table = value;
                ArgsMatcher = table.HasHeader?
                    new HeaderTableArgsMatcher(this) as ArgsMatcher:
                    new TableArgsMatcher(this);
            }
        }

        public void AddRow(List<string> Row)
        {
            if (!HasTable) Table = new Table();

            if (Table.IsSingleColumn && !Table.HasHeader) 
                Table.Rows[0].Add(Row[0]);
            else Table.Add(Row);
        }

        public bool HasObjectImplementation
        {
            get
            {
                return IsImplemented 
                    && HasTable 
                    && Table.HasHeader 
                    && Implementation.HasObjectArgFor(this);
            } 
        }

        public Type ObjectArg { get { return Implementation.LastArg(); } }

        public ArgsMatcher ArgsMatcher;
    }
}