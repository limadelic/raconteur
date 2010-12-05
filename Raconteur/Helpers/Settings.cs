namespace Raconteur
{
    public static class Settings
    {
        public static string XUnit
        {
            get { return XUnitSettings.XUnit; } 
            set { XUnitSettings.XUnit = value; }
        }

        public static Language Language = Languages.In["en"];
    }
}