using System.Collections.Generic;
using System.Reflection;

namespace Raconteur
{
    public class StepImplementation
    {
        public MethodInfo Method { get; set; }

        public List<Step> Steps { get; set; }
    }
}