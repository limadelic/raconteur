using System.Collections.Generic;

namespace Raconteur
{
    public class Language
    {
        public string Name, Native, Feature, Background, Scenario, ScenarioOutline, Examples;
    }

    public static class Languages
    {
        public static readonly Dictionary<string, Language> All = new Dictionary<string, Language>
        {
            { "en", new Language
            {
                Name = "English",
                Native = "English",
                Feature = "Feature",
                Background = "Background",
                Scenario = "Scenario",
                ScenarioOutline = "Scenario Outline",
                Examples = "Examples",
            }},

            { "ar", new Language
            {
                Name = "Arabic",
                Native = "العربية",
                Feature = "خاصية",
                Background = "الخلفية",
                Scenario = "سيناريو",
                ScenarioOutline = "سيناريو مخطط",
                Examples = "امثلة"
            }},

            { "es", new Language
            {
                Name = "Spanish",
                Native = "Español",
                Feature = "Característica",
                Background = "Antecedentes",
                Scenario = "Escenario",
                ScenarioOutline = "Esquema del escenario",
                Examples = "Ejemplos"
            }},

            { "ru", new Language
            {
                Name = "Russian",
                Native = "русский",
                Feature = "Функционал",
                Background = "Предыстория",
                Scenario = "Сценарий",
                ScenarioOutline = "Структура сценария",
                Examples = "Значения"
            }},
        };

        public static Language Current = All["en"];
    }
}