using System;
using System.Reflection;

namespace Raconteur.Compilers
{
    public abstract class ArgsMatcherBase : ArgsMatcher
    {
        protected abstract bool CouldMatch { get; }
        protected abstract bool Matches { get; }
        
        public bool IsMatch
        {
            get { return CouldMatch && Matches; }
        }

        bool ArgsMatcher.Matches(MethodInfo Method)
        {
            return false;
        }

        public StepCompiler StepCompiler { get; set; }

        protected ArgsMatcherBase(StepCompiler StepCompiler)
        {
            this.StepCompiler = StepCompiler;
        }

        protected Step Step
        {
            get { return StepCompiler.Step; } 
            set { StepCompiler.Step = value;}
        }

        protected MethodInfo Method { get { return StepCompiler.Method; } }

        protected int ArgsCount { get { return Method.GetParameters().Length; } }
    }
}