using System;
using System.Collections.Generic;
using Raconteur.Helpers;

namespace Raconteur.Compilers
{
    public static class ArgFormatter 
    {
        public static string Format(string Arg)
        {
            if (Arg.IsDateTime()) return @"System.DateTime.Parse(""" + Arg + @""")";

            if (Arg.IsNumeric() || Arg.IsBoolean() || Arg == "null") 
                return Arg;
            
            if (Arg.IsMultiline()) 
                return Environment.NewLine + @"@""" + Arg + '"';

            return '"' + Arg + '"';
        }

        static readonly Dictionary<Type, Func<string, string>> TypeFormatters = new Dictionary<Type, Func<string, string>>
        {
            { typeof(string), s => s.Quoted() },
            { typeof(DateTime), s => @"System.DateTime.Parse(""" + s + @""")" },
        };

        public static string Format(string Arg, Type Type)
        {
            return TypeFormatters.ContainsKey(Type)?
                TypeFormatters[Type](Arg) : Arg;
        }
    }
}