using System;
using System.Collections.Generic;

namespace Raconteur
{
    public class Setting
    {
        public XUnit XUnit = XUnits.Framework["mstest"];

        public Language Language = Languages.In["en"];

        public List<string> StepLibraries = new List<string>();
    }

    public static class Settings
    {
        static Settings() { Project = DefaultProject; }

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

        static Setting Setting { get { return SettingFor[project]; } }

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

        public static List<string> StepLibraries
        {
            get { return Setting.StepLibraries; }
            set { Setting.StepLibraries = value;}
        }
    }
}