using FluentSpec;
using MbUnit.Framework;

namespace Specs
{
    public class BehaviourOf<T> : BehaviorOf<T>
    {
        [SetUp]
        public void SetUp()
        {
            base.Setup();
        }
    }
}