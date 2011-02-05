using System;
using System.Collections.Generic;
using System.Dynamic;

namespace PlayDohs
{
    public class PlayDoh : DynamicObject
    {
        internal Processor Processor;
        internal object ReturnValue;
        internal Action Action;
        internal readonly List<Call> ExpectedCalls;

        public PlayDoh()
        {
            Processor = new Default(this);
            ExpectedCalls = new List<Call>();
            Properties = new Dictionary<string, object>();
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            return Processor.TryInvokeMember(binder, args, out result);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result) 
        {
            return Processor.TryGetMember(binder, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value) 
        {
            return Processor.TrySetMember(binder, value);
        }

        public PlayDoh On
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

        public PlayDoh Does(Action action)
        {
            Action = action;
            return this;
        }

        Dictionary<string, object> Properties;

        internal object this[string property] 
        { 
            get { return Properties[property];  }
            set { Properties[property] = value; } 
        }
    }
}