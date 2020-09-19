using System;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace TweakAsSharp
{
    public class Parser
    {
        private static readonly Script<object> baseScript = CSharpScript.Create("");

        public static bool UseRoslyn = true;

        static Parser()
        {
            Console.Write("Warming up the runtime...");
            Evaluate("42");    // warm up
            Console.WriteLine(" Done\n=======");
        }

        public static object Evaluate(string code)
        {
            return baseScript.ContinueWith(code).RunAsync().Result.ReturnValue;
        }
    }
}