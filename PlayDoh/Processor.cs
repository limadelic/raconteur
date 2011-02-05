using System.Dynamic;

namespace PlayDohs
{
    public interface Processor
    {
        bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result);
    }
}