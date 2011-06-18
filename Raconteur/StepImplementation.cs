using System.Collections.Generic;
using System.Reflection;

namespace Raconteur
{
    public class StepImplementation
    {
        public StepImplementation()
        {
            Steps = new List<Step>();
        }

        public MethodInfo Method { get; set; }

        public List<Step> Steps { get; set; }
    }
}