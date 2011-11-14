using System;
using System.Dynamic;

namespace PlayDohs
{
    public class Record : Processor
    {
        readonly PlayDoh Target;

        public Record(dynamic target) { Target = target; }

        public bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            Target.Processor = new Default(Target);
            Target.ExpectedCalls.Add(new Call
            {
                Name = binder.Name,
                Args = args,
                Result = Target.ReturnValue,
                Action = Target.Action
            });

            result = Target;
            return true;
        }

        public bool TrySetMember(SetMemberBinder binder, object value)
        {
            throw new NotImplementedException();
        }

        public bool TryGetMember(GetMemberBinder binder, out object result)
        {
            throw new NotImplementedException();
        }
    }
}