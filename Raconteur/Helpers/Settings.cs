namespace Raconteur
{
    public class Settings
    {
        static string xUnit;
        public static string XUnit
        {
            get { return xUnit; } 
            set { xUnit = value.ToUpper(); }
        }
    }
}