using System.Collections.Generic;

namespace Raconteur.Generators.Steps
{
    public abstract class StepGenerator : CodeGenerator
    {
        public abstract IEnumerable<string> FormatArgsOnly { get; }
        public abstract IEnumerable<string> FormatArgsForTable { get; }

        public const string StepTemplate = 
            @"        
            {0}({1});";

        public const string MultilineStepExecution = 
            @"        
            {0}
            ({1}
            );";

        public const string StepRowExecution = 
            @"        
                new[] {{{0}}},";

        public readonly Step Step;
        public IEnumerable<string> Row;

        protected StepGenerator(Step Step)
        {
            this.Step = Step;
        }

        public CodeGenerator CodeGenerator { get; set; }
        
        public string Code { get { return CodeGenerator.Code; } }
    }
}