namespace Raconteur.Compilers
{
    public class ObjectTableArgsMatcher : ArgsMatcherBase
    {
        public ObjectTableArgsMatcher(StepCompiler StepCompiler) : base(StepCompiler) {}

        protected override bool CouldMatch
        {
            get
            {
                return Step.HasTable 
                    && Step.Table.HasHeader 
                    && Method.HasObjectArgFor(Step);
            }
        }

        protected override bool Matches 
        {
            get { return ArgsCount == Step.Args.Count + 1; }
        }
    }
}