namespace Raconteur.Compilers
{
    public class TableArgsMatcher : ArgsMatcherBase
    {
        public TableArgsMatcher(Step Step)
        {
            this.Step = Step;    
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