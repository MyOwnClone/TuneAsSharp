using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace TweakAsSharp
{
    static class TweakAS
    {
        // not thread safe I guess
        private static readonly Script<object> _mBaseScript = CSharpScript.Create("");
        private static readonly string _mTokenToLookFor = "t" + "v";
        private static readonly Dictionary<string, object> _mValueDictionary = new Dictionary<string, object>();

        static TweakAS()
        {
            Console.Write("Warming up the runtime...");
            Evaluate("42");    // warm up
            Console.WriteLine(" Done");
        }
        
        public static Tuple<string, int, int> GetFileLineColumnInfo(int frameIndex = 1)
        {
            var st = new StackTrace(true);

            var frame = st.GetFrame(frameIndex);

            if (frame != null)
            {
                var filename = frame.GetFileName();
                var line = frame.GetFileLineNumber();
                var column = frame.GetFileColumnNumber();
        
                return new Tuple<string, int, int>(filename, line, column);
            }

            return null;
        }

        public static string ConvertFileLineColumnInfo2StringHash(Tuple<string, int, int> info)
        {
            return $"{info.Item1}:{info.Item2}:{info.Item3}";
        }

        // not thread safe I guess
        public static object Evaluate(string code)
        {
            return _mBaseScript.ContinueWith(code).RunAsync().Result.ReturnValue;
        }

        public static object tv(object defaultValue)
        {
            //Console.WriteLine(ConvertFileLineColumnInfo2StringHash(GetFileLineColumnInfo(2)));

            object value = ResolveValue(GetFileLineColumnInfo(2), defaultValue);

            return value;
        }

        private static object ResolveValue(Tuple<string,int,int> info, object defaultValue)
        {
            var filename = info.Item1;

            var allLines = System.IO.File.ReadAllLines(filename);

            if (allLines.Length == 0)
            {
                return defaultValue;
            }
            
            //Console.WriteLine($"{allLines.Length}");

            var lineNumber = info.Item2;
            
            //Console.WriteLine($"{lineNumber} / {allLines.Length}");

            var line = allLines[lineNumber - 1];
            
            //Console.WriteLine($"{line}");

            if (line.Contains(_mTokenToLookFor))
            {
                var index = line.IndexOf(_mTokenToLookFor);

                var lengthToSkip = _mTokenToLookFor.Length;

                var braceIndex = line.IndexOf("(", index + lengthToSkip);

                if (braceIndex == -1)
                {
                    return 0;
                }

                var closingBraceIndex = line.IndexOf(")", braceIndex + 1);

                if (closingBraceIndex == -1)
                {
                    return 0;
                }

                var valueString = line.Substring(braceIndex + 1, closingBraceIndex - braceIndex - 1);
                
                //Console.WriteLine(valueString);

                return Evaluate(valueString);
            }

            return 0;
        }
    }
}

// ReSharper disable once CheckNamespace