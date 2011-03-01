namespace Common
{
    public class StepLibrary
    {
        public void Step(){}
        public void Step_from_Lib(){}
    }

    public class StepLibraryInSameNamespace
    {
        public void Step(){}
        public void Step_from_Lib(){}
    }
}

namespace Uncommon
{
    public class AnotherStepLibrary
    {
        public void another_Step(string Arg){}
        public void Step_from_another_Lib(){}
    }
}
