namespace Raconteur.Compilers
{
    public class HeaderTableArgsMatcher : ArgsMatcherBase
    {
        public HeaderTableArgsMatcher(Step Step)
        {
            this.Step = Step;    
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