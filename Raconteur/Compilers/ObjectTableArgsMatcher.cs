namespace Raconteur.Compilers
{
    public class ObjectTableArgsMatcher : ArgsMatcherBase
    {
        protected override bool Matches 
        {
            get { return ArgsCount == Step.Args.Count + 1; }
        }
    }
}