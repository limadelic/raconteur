namespace Raconteur.Helpers
{
    public static class Extensions
    {
        public static void Debug(this object o)
        {
            System.Diagnostics.Debugger.Launch();
        }

        public static void Break(this object o)
        {
            System.Diagnostics.Debugger.Break();
        }
    }
}