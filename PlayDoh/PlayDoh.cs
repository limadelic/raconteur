using System.Collections.Generic;
using System.Dynamic;

namespace PlayDohs
{
    public class PlayDoh : DynamicObject
    {
        internal Processor Processor;
        internal object ReturnValue;
        internal readonly List<Call> ExpectedCalls;

        public PlayDoh()
        {
            Processor = new Default(this);
            ExpectedCalls = new List<Call>();
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            return Processor.TryInvokeMember(binder, args, out result);
        }

        public PlayDoh When
        {
            get
            {
                Processor = new Record(this);
                return this;
            }
        }

        public PlayDoh Returns(object value)
        {
            ReturnValue = value;
            return this;
        }
    }
}