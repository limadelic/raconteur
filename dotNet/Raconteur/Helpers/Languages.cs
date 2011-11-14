using System.Collections.Generic;

namespace Raconteur.Helpers
{
    public class Language
    {
        public string Name, Native, Feature, Using = "using", Background, Scenario, ScenarioOutline, Examples;

        public List<string> Keywords
        {
            get { return new List<string> {Feature, Scenario, ScenarioOutline, Examples}; }
        }
    }

    public static class Languages
    {
        public static readonly Dictionary<string, Language> In = new Dictionary<string, Language>
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

            { "bg", new Language
            {
                Name = "Bulgarian",
                Native = "български",
                Feature = "Функционалност",
                Background = "Предистория",
                Scenario = "Сценарий",
                ScenarioOutline = "Рамка на сценарий",
                Examples = "Примери"
            }},

            { "ca", new Language
            {
                Name = "Catalan",
                Native = "Català",
                Feature = "Característica",
                Background = "Rerefons",
                Scenario = "Escenari",
                ScenarioOutline = "Esquema de l'escenari",
                Examples = "Exemples"
            }},

            { "cy-GB", new Language
            {
                Name = "Welsh",
                Native = "Cymraeg",
                Feature = "Arwedd",
                Background = "Cefndir",
                Scenario = "Scenario",
                ScenarioOutline = "Scenario Amlinellol",
                Examples = "Enghreifftiau"
            }},

            { "cs", new Language
            {
                Name = "Czech",
                Native = "Česky",
                Feature = "Požadavek",
                Background = "Pozadí",
                Scenario = "Scénář",
                ScenarioOutline = "Náčrt Scénáře",
                Examples = "Příklady"
            }},

            { "da", new Language
            {
                Name = "Danish",
                Native = "Dansk",
                Feature = "Egenskab",
                Background = "Baggrund",
                Scenario = "Scenarie",
                ScenarioOutline = "Abstrakt Scenario",
                Examples = "Eksempler"
            }},

            { "de", new Language
            {
                Name = "German",
                Native = "Deutsch",
                Feature = "Funktionalität",
                Background = "Grundlage",
                Scenario = "Szenario",
                ScenarioOutline = "Szenariogrundriss",
                Examples = "Beispiele"
            }},

            { "eo", new Language
            {
                Name = "Esperanto",
                Native = "Esperanto",
                Feature = "Trajto",
                Background = "Fono",
                Scenario = "Scenaro",
                ScenarioOutline = "Konturo de la scenaro",
                Examples = "Ekzemploj"
            }},

            { "es", new Language
            {
                Name = "Spanish",
                Native = "Español",
                Feature = "Funcionalidad",
                Using = "usando",
                Background = "Antecedentes",
                Scenario = "Escenario",
                ScenarioOutline = "Esquema del escenario",
                Examples = "Ejemplos"
            }},

            { "et", new Language
            {
                Name = "Estonian",
                Native = "Eesti Keel",
                Feature = "Omadus",
                Background = "Taust",
                Scenario = "Stsenaarium",
                ScenarioOutline = "Raamstsenaarium",
                Examples = "Juhtumid"
            }},

            { "fi", new Language
            {
                Name = "Finnish",
                Native = "Suomi",
                Feature = "Ominaisuus",
                Background = "Tausta",
                Scenario = "Tapaus",
                ScenarioOutline = "Tapausaihio",
                Examples = "Tapaukset"
            }},

            { "fr", new Language
            {
                Name = "French",
                Native = "Français",
                Feature = "Fonctionnalité",
                Background = "Contexte",
                Scenario = "Scénario",
                ScenarioOutline = "Plan du Scénario",
                Examples = "Exemples"
            }},

            { "he", new Language
            {
                Name = "Hebrew",
                Native = "עברית",
                Feature = "תכונה",
                Background = "רקע",
                Scenario = "תרחיש",
                ScenarioOutline = "תבנית תרחיש",
                Examples = "דוגמאות"
            }},

            { "hr", new Language
            {
                Name = "Croatian",
                Native = "Hrvatski",
                Feature = "Osobina",
                Background = "Pozadina",
                Scenario = "Scenarij",
                ScenarioOutline = "Skica",
                Examples = "Primjeri"
            }},

            { "hu", new Language
            {
                Name = "Hungarian",
                Native = "Magyar",
                Feature = "Jellemző",
                Background = "Háttér",
                Scenario = "Forgatókönyv",
                ScenarioOutline = "Forgatókönyv vázlat",
                Examples = "Példák"
            }},

            { "id", new Language
            {
                Name = "Indonesian",
                Native = "Bahasa Indonesia",
                Feature = "Fitur",
                Background = "Dasar",
                Scenario = "Skenario",
                ScenarioOutline = "Skenario konsep",
                Examples = "Contoh"
            }},

            { "it", new Language
            {
                Name = "Italian",
                Native = "Italiano",
                Feature = "Funzionalità",
                Background = "Contesto",
                Scenario = "Scenario",
                ScenarioOutline = "Schema dello scenario",
                Examples = "Esempi"
            }},

            { "ja", new Language
            {
                Name = "Japanese",
                Native = "日本語",
                Feature = "フィーチャ",
                Background = "背景",
                Scenario = "シナリオ",
                ScenarioOutline = "シナリオアウトライン",
                Examples = "例"
            }},

            { "ko", new Language
            {
                Name = "Korean",
                Native = "한국어",
                Feature = "기능",
                Background = "배경",
                Scenario = "시나리오",
                ScenarioOutline = "시나리오 개요",
                Examples = "예"
            }},

            { "lt", new Language
            {
                Name = "Lithuanian",
                Native = "Lietuvių Kalba",
                Feature = "Savybė",
                Background = "Kontekstas",
                Scenario = "Scenarijus",
                ScenarioOutline = "Scenarijaus šablonas",
                Examples = "Pavyzdžiai"
            }},

            { "lu", new Language
            {
                Name = "Luxemburgish",
                Native = "Lëtzebuergesch",
                Feature = "Funktionalitéit",
                Background = "Hannergrond",
                Scenario = "Szenario",
                ScenarioOutline = "Plang vum Szenario",
                Examples = "Beispiller"
            }},

            { "lv", new Language
            {
                Name = "Latvian",
                Native = "Latviešu",
                Feature = "Funkcionalitāte",
                Background = "Konteksts",
                Scenario = "Scenārijs",
                ScenarioOutline = "Scenārijs pēc parauga",
                Examples = "Piemēri"
            }},

            { "nl", new Language
            {
                Name = "Dutch",
                Native = "Nederlands",
                Feature = "Functionaliteit",
                Background = "Achtergrond",
                Scenario = "Scenario",
                ScenarioOutline = "Abstract Scenario",
                Examples = "Voorbeelden"
            }},

            { "no", new Language
            {
                Name = "Norwegian",
                Native = "Norsk",
                Feature = "Egenskap",
                Background = "Bakgrunn",
                Scenario = "Scenario",
                ScenarioOutline = "Scenariomal",
                Examples = "Eksempler"
            }},

            { "pl", new Language
            {
                Name = "Polish",
                Native = "Polski",
                Feature = "Właściwość",
                Background = "Założenia",
                Scenario = "Scenariusz",
                ScenarioOutline = "Szablon scenariusza",
                Examples = "Przykłady"
            }},

            { "pt", new Language
            {
                Name = "Portuguese",
                Native = "Português",
                Feature = "Funcionalidade",
                Background = "Contexto",
                Scenario = "Cenário",
                ScenarioOutline = "Esquema do Cenário",
                Examples = "Exemplos"
            }},

            { "ro", new Language
            {
                Name = "Romanian",
                Native = "Română",
                Feature = "Conditii",
                Background = "Functionalitate",
                Scenario = "Scenariu",
                ScenarioOutline = "Scenariul de sablon",
                Examples = "Exemplele"
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

            { "sv", new Language
            {
                Name = "Swedish",
                Native = "Svenska",
                Feature = "Egenskap",
                Background = "Bakgrund",
                Scenario = "Scenario",
                ScenarioOutline = "Abstrakt Scenario",
                Examples = "Exempel"
            }},

            { "sk", new Language
            {
                Name = "Slovak",
                Native = "Slovensky",
                Feature = "Požiadavka",
                Background = "Pozadie",
                Scenario = "Scenár",
                ScenarioOutline = "Náčrt Scenáru",
                Examples = "Príklady"
            }},

            { "sr-Latn", new Language
            {
                Name = "Serbian (Latin)",
                Native = "Srpski (Latinica)",
                Feature = "Funkcionalnost",
                Background = "Kontekst",
                Scenario = "Scenario",
                ScenarioOutline = "Struktura scenarija",
                Examples = "Primeri"
            }},

            { "sr-Cyrl", new Language
            {
                Name = "Serbian",
                Native = "Српски",
                Feature = "Функционалност",
                Background = "Контекст",
                Scenario = "Сценарио",
                ScenarioOutline = "Структура сценарија",
                Examples = "Примери"
            }},

            { "tr", new Language
            {
                Name = "Turkish",
                Native = "Türkçe",
                Feature = "Özellik",
                Background = "Geçmiş",
                Scenario = "Senaryo",
                ScenarioOutline = "Senaryo taslağı",
                Examples = "Örnekler"
            }},

            { "uk", new Language
            {
                Name = "Ukrainian",
                Native = "Українська",
                Feature = "Функціонал",
                Background = "Передумова",
                Scenario = "Сценарій",
                ScenarioOutline = "Структура сценарію",
                Examples = "Приклади"
            }},

            { "uz", new Language
            {
                Name = "Uzbek",
                Native = "Узбекча",
                Feature = "Функционал",
                Background = "Тарих",
                Scenario = "Сценарий",
                ScenarioOutline = "Сценарий структураси",
                Examples = "Мисоллар"
            }},

            { "vi", new Language
            {
                Name = "Vietnamese",
                Native = "Tiếng Việt",
                Feature = "Tính năng",
                Background = "Bối cảnh",
                Scenario = "Tình huống",
                ScenarioOutline = "Khung tình huống",
                Examples = "Dữ liệu"
            }},

            { "zh-CN", new Language
            {
                Name = "Chinese simplified",
                Native = "简体中文",
                Feature = "功能",
                Background = "背景",
                Scenario = "场景",
                ScenarioOutline = "场景大纲",
                Examples = "例子"
            }},

            { "zh-TW", new Language
            {
                Name = "Chinese traditional",
                Native = "繁體中文",
                Feature = "功能",
                Background = "背景",
                Scenario = "場景",
                ScenarioOutline = "場景大綱",
                Examples = "例子"
            }},
        };
    }
}