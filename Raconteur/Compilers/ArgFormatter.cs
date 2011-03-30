using System;
using Raconteur.Helpers;

namespace Raconteur.Compilers
{
    public static class ArgFormatter 
    {
        public static string ValueOf(string Arg)
        {
            if (Arg.IsDateTime()) return @"System.DateTime.Parse(""" + Arg + @""")";

            if (Arg.IsNumeric() || Arg.IsBoolean() || Arg == "null") 
                return Arg;
            
            if (Arg.IsMultiline()) 
                return Environment.NewLine + @"@""" + Arg + '"';

            return '"' + Arg + '"';
        }
    }
}