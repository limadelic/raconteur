namespace Raconteur.Compilers
{
    public class SimpleArgsMatcher : ArgsMatcherBase
    {
        public SimpleArgsMatcher(Step Step)
        {
            this.Step = Step;    
        }

        protected override bool Matches
        {
            get { return ArgsCount == Step.Args.Count; }
        }
    }
}