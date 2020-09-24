using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace TuneAsSharp
{
    public static class Parser
    {
        private static readonly Script<object> BaseScript = CSharpScript.Create("");

        public static bool UseRoslyn = true;
        private static bool _warmUpDone;

        private static object EvaluateWithRoslyn(string code)
        {
            return BaseScript.ContinueWith(code).RunAsync().Result.ReturnValue;
        }

        public static object Evaluate(string code)
        {
            // ReSharper disable once InvertIf
            if (UseRoslyn && !_warmUpDone)
            {
                Console.Write("Warming up the runtime...");

                EvaluateWithRoslyn(code); // warm up

                Console.WriteLine(" Done\n=======");

                _warmUpDone = true;
            }

            return UseRoslyn ? EvaluateWithRoslyn(code) : Parse(code);
        }

        private static bool IsInt(string code)
        {
            return code.All(char.IsDigit);
        }

        private static bool IsFloat(string code)
        {
            return code.All(c => char.IsDigit(c) || c == 'f' || c == '.');
        }

        private static bool IsString(string code)
        {
            return code.StartsWith("\"") && code.EndsWith("\"");
        }

        private static object Parse(string code)
        {
            if (IsInt(code))
            {
                return int.Parse(code);
            }

            if (IsFloat(code))
            {
                code = code.Replace("f", ""); // TODO: ReplaceAll()

                return float.Parse(code);
            }

            if (IsString(code))
            {
                return code.Substring(1, code.Length - 2);
            }

            throw new NotImplementedException("Unexpected type!!!");
        }
    }
}