namespace Raconteur.Compilers
{
    public class TableArgsMatcher : ArgsMatcherBase
    {
        public TableArgsMatcher(StepCompiler StepCompiler) : base(StepCompiler) {}

        protected override bool CouldMatch
        {
            get { return Step.HasTable && !Step.Table.HasHeader; }
        }

        protected override bool Matches 
        {
            get 
            {
                return (ArgsCount == Step.Args.Count + 1)
                    && Method.HasTableArg();
            }
        }
    }
}