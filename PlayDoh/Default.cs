using System.Dynamic;

namespace PlayDohs
{
    public class Default : Processor
    {
        readonly PlayDoh Target;

        public Default(dynamic target) { Target = target; }

        public bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var expectedCall = Target.ExpectedCalls.FindLast
                (
                    call => call.Matches(binder.Name, args)
                );

            result = expectedCall != null ? expectedCall.Result : Target;

            return true;
        }
    }
}