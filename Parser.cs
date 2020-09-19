using System;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace TweakAsSharp
{
    public static class Parser
    {
        private static readonly Script<object> baseScript = CSharpScript.Create("");

        public static bool UseRoslyn = true;
        private static bool WarmUpDone;

        private static object EvaluateInner(string code)
        {
            return baseScript.ContinueWith(code).RunAsync().Result.ReturnValue;
        }

        public static object Evaluate(string code)
        {
            if (UseRoslyn && !WarmUpDone)
            {
                Console.Write("Warming up the runtime...");

                EvaluateInner(code);   // warm up
                
                Console.WriteLine(" Done\n=======");

                WarmUpDone = true;
            }
            
            return UseRoslyn ? EvaluateInner(code) : Parse(code);
        }

        private static bool IsInt(string code)
        {
            foreach (var c in code)
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }
            
            return true;
        }

        private static bool IsFloat(string code)
        {
            foreach (var c in code)
            {
                if (!Char.IsDigit(c) && c != 'f' && c != '.')
                {
                    return false;
                }
            }
            
            return true;
        }

        private static bool IsString(string code)
        {
            return code.StartsWith("\"") && code.EndsWith("\"");
        }

        private static object Parse(string code)
        {
            if (IsInt(code))
            {
                return (int) int.Parse(code);
            }
            else if (IsFloat(code))
            {
                code = code.Replace("f", "");
                
                return (float) float.Parse(code);
            }
            else if (IsString(code))
            {
                return code.Substring(1, code.Length - 2);
            }
            else
                throw new NotImplementedException("Unexpected type!!!");
        }
    }
}