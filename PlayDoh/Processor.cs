using System.Dynamic;

namespace PlayDohs
{
    public interface Processor
    {
        bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result);
        bool TrySetMember(SetMemberBinder binder, object value);
        bool TryGetMember(GetMemberBinder binder, out object result);
    }
}