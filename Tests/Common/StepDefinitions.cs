using System.Linq;
using System.Reflection;

namespace Common
{
    public class StepDefinitions
    {
        public void Step(){}
        public void Step(string Overloaded){}
        public void Step(int Overloaded){}

        public static MethodInfo StepMethod;
        public static MethodInfo StepOverloaded;
        public static MethodInfo StepOverloadedInt;
        
        static StepDefinitions()
        {
            var Methods = typeof(StepDefinitions).GetMethods()
                .Where(m => m.Name == "Step").ToList();

            StepMethod = Methods[0];
            StepOverloaded = Methods[1];
            StepOverloadedInt = Methods[2];
        }

        public void Step_from_Lib(){}
        public void Step_from_Step_Definitions(){}
    }

    public class StepDefinitionsInSameNamespace
    {
        public void Step(){}
        public void Step_from_Lib(){}
    }

    public class StepDefinitionsInLibrary
    {
        public void Step_from_Step_Definitions_in_Library(){}
    }
}

namespace Uncommon
{
    public class AnotherStepDefinitions
    {
        public void another_Step(string Arg){}

        public static MethodInfo AnotherStepMethod;
        
        static AnotherStepDefinitions()
        {
            AnotherStepMethod = typeof(AnotherStepDefinitions)
                .GetMethod("another_Step");
        }
        public void Step_from_another_Lib(){}
        public void Step_from_another_Step_Definitions(){}
    }
}
