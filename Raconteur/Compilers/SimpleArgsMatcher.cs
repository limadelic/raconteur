namespace Raconteur.Compilers
{
    public class SimpleArgsMatcher : ArgsMatcherBase
    {
        public SimpleArgsMatcher(StepCompiler StepCompiler) : base(StepCompiler) {}
        
        public SimpleArgsMatcher(Step Step) : base(new StepCompiler())
        {
            this.Step = Step;    
        }

        protected override bool CouldMatch
        {
            get { return !Step.HasTable; }
        }

        protected override bool Matches
        {
            get { return ArgsCount == Step.Args.Count; }
        }
    }
}