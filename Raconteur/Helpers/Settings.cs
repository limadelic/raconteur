using System.Collections.Generic;

namespace Raconteur.Helpers
{
    public class Setting
    {
        public XUnit XUnit = XUnits.Framework["mstest"];

        public Language Language = Languages.In["en"];

        public List<string> StepDefinitions = new List<string>();

        public List<string> Libraries = new List<string>();
    }

    public static class Settings
    {
        static Settings() { Project = DefaultProject; }

        public const string File = "raconteur.config";

        static readonly object DefaultProject = new object();
        public static void RestoreDefaults() { project = DefaultProject; }

        static object project;
        public static object Project
        {
            get { return project; }
            set
            {
                if (value == null) value = DefaultProject;

                if (!SettingFor.ContainsKey(value)) 
                    SettingFor[value] = new Setting();

                project = value;
            }
        }

        static readonly Dictionary<object, Setting> SettingFor = new Dictionary<object, Setting>();

        public static Setting Setting { get { return SettingFor[project]; } }

        public static XUnit XUnit
        {
            get { return Setting.XUnit; } 
            set { Setting.XUnit = value; } 
        }

        public static Language Language
        {
            get { return Setting.Language; } 
            set { Setting.Language = value; } 
        }

        public static List<string> StepDefinitions
        {
            get { return Setting.StepDefinitions; }
            set { Setting.StepDefinitions = value;}
        }

        public static List<string> Libraries
        {
            get { return Setting.Libraries; }
            set { Setting.Libraries = value;}
        }
    }
}