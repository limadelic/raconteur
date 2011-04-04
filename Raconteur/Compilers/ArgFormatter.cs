using System;
using System.Collections.Generic;
using Raconteur.Helpers;

namespace Raconteur.Compilers
{
    public static class ArgFormatter 
    {
        public static string Format(string Arg)
        {
            if (Arg.IsDateTime()) return Arg.FormatDateTime();

            if (Arg.IsNumeric() || Arg.IsBoolean() || Arg == "null") 
                return Arg;
            
            return Arg.FormatString();
        }

        static readonly Dictionary<Type, Func<string, string>> TypeFormatters = new Dictionary<Type, Func<string, string>>
        {
            { typeof(string), s => s.FormatString() },
            { typeof(DateTime), s => s.FormatDateTime() },
        };

        public static string Format(string Arg, Type Type)
        {
            return TypeFormatters.ContainsKey(Type)?
                TypeFormatters[Type](Arg) : Arg;
        }

        public static string FormatString(this string Arg)
        {
            return Arg.IsMultiline() ? Arg.FormatMultilineString() : Arg.Quoted();
        }

        public static string FormatMultilineString(this string Arg)
        {
            return Environment.NewLine + @"@""" + Arg + '"';
        }

        public static string FormatDateTime(this string Arg)
        {
            return @"System.DateTime.Parse(""" + Arg + @""")";
        }
    }
}