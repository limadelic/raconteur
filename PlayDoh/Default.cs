using System;
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

            if (expectedCall == null) result = Target;
            else
            {
                if (expectedCall.Action != null) expectedCall.Action();
                result = expectedCall.Result;
            }

            return true;
        }

        public bool TrySetMember(SetMemberBinder binder, object value)
        {
            Target[binder.Name] = value;
            return true;
        }

        public bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = Target[binder.Name] ?? Target;
            return true;
        }
    }
}