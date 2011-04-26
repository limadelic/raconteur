namespace Raconteur.Compilers
{
    public class HeaderTableArgsMatcher : ArgsMatcherBase
    {
        public HeaderTableArgsMatcher(StepCompiler StepCompiler) : base(StepCompiler) {}

        protected override bool CouldMatch
        {
            get
            {
                return Step.HasTable 
                    && Step.Table.HasHeader 
                    && !Method.HasObjectArgFor(Step);
            }
        }

        protected override bool Matches 
        {
            get
            {
                return ArgsCount == Step.Args.Count + Step.Table.Header.Count;
            }
        }
    }
}