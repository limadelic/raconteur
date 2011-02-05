using System.Dynamic;
using System.Linq;

namespace PlayDohs
{
    public class Call 
    {
        public InvokeBinder Binder;

        public string Name;
        public object[] Args;
        public object Result;

        public bool Matches(InvokeBinder binder, object[] args)
        {
            return Matches(binder) && Matches(args);
        }

        public bool Matches(string name, object[] args)
        {
            return Name.Equals(name) && Matches(args);
        }

        bool Matches(InvokeBinder binder)
        {
            if (Binder == null) return false;

            return Binder.CallInfo == binder.CallInfo;
        }

        bool Matches(object[] args)
        {
            return !Args.Where((T, i) => !args[i].Equals(T)).Any();
        }
    }
}