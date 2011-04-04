using System.Linq;
using System.Reflection;

namespace Common
{
    public class StepDefinitions
    {
        public void Step(){}
        public void Step(string Overloaded){}
        public void Step(int Overloaded){}
        public void Step_with_object(User User){}
        public void Step(string One, string Another){}
        public void Step(params string[][] Table){}
        public void Step(string Arg, params string[][] Table){}

        public static MethodInfo StepMethod;
        public static MethodInfo StepOverloaded;
        public static MethodInfo StepOverloadedInt;
        public static MethodInfo StepWithObject;
        public static MethodInfo StepWithTwoArgs;
        public static MethodInfo StepWithTable;
        public static MethodInfo StepWithTableAndArg;

        static StepDefinitions()
        {
            var Methods = typeof(StepDefinitions).GetMethods()
                .Where(m => m.Name.StartsWith("Step")).ToList();

            StepMethod = Methods[0];
            StepOverloaded = Methods[1];
            StepOverloadedInt = Methods[2];
            StepWithObject = Methods[3];
            StepWithTwoArgs = Methods[4];
            StepWithTable = Methods[5];
            StepWithTableAndArg = Methods[6];
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

    public class User { public string Name, Pass; }
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
