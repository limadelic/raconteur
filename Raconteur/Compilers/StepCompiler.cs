using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Raconteur.Helpers;

namespace Raconteur.Compilers
{
    public class StepCompiler
    {
        public MethodInfo Method;
        public Step Step;

        public bool Matches(MethodInfo Method, Step Step)
        {
            this.Method = Method;
            this.Step = Step;

            return MatchesName && MatchesArgs;
        }

        bool MatchesName
        {
            get { return Method.Name == Step.Name; }
        }

        bool MatchesArgs
        {
            get
            {
                var ExpectsArgs = Step.HasArgs || Step.HasTable;
                var HasArgs = Method.HasArgs();

                if (!ExpectsArgs && !HasArgs) return true;
                if (ExpectsArgs ^ HasArgs) return false;

                return Step.ArgsMatcher.Matches(Method);
            }
        }
    }
}