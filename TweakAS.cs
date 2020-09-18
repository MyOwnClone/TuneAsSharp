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
        
        public static Tuple<string, int, int, int> GetFileLineColumnInfo(int frameIndex = 1)
        {
            var st = new StackTrace(true);

            var frame = st.GetFrame(frameIndex);

            if (frame != null)
            {
                var filename = frame.GetFileName();
                var line = frame.GetFileLineNumber();
                var column = frame.GetFileColumnNumber();

                var ilOffset = frame.GetILOffset();
        
                return new Tuple<string, int, int, int>(filename, line, column, ilOffset);
            }

            return null;
        }

        public static string ConvertFileLineColumnInfo2StringHash(Tuple<string, int, int, int> info)
        {
            return $"{info.Item1}:{info.Item2}:{info.Item3}:{info.Item4}";
        }

        // not thread safe I guess
        public static object Evaluate(string code)
        {
            return _mBaseScript.ContinueWith(code).RunAsync().Result.ReturnValue;
        }

        public static object tv(object defaultValue)
        {
            object value = ResolveValue(GetFileLineColumnInfo(2), defaultValue, GetCallCount());

            return value;
        }

        private static object ResolveValue(Tuple<string,int,int,int> info, object defaultValue, int matchIndex = 0)
        {
            var filename = info.Item1;

            var allLines = System.IO.File.ReadAllLines(filename);

            if (allLines.Length == 0)
            {
                return defaultValue;
            }

            var lineNumber = info.Item2;
            
            var columnNumber = info.Item3;

            var lineString = allLines[lineNumber - 1];

            if (lineString.Contains(_mTokenToLookFor))
            {
                int index = columnNumber;
                string valueString = string.Empty;

                int matchCounter = 0;

                do
                {
                    index = lineString.IndexOf(_mTokenToLookFor, index, StringComparison.Ordinal);

                    if (index == -1)
                    {
                        break;
                    }

                    var lengthToSkip = _mTokenToLookFor.Length;

                    var braceIndex = lineString.IndexOf("(", index + lengthToSkip, StringComparison.Ordinal);

                    if (braceIndex == -1)
                    {
                        break;
                    }

                    var closingBraceIndex = lineString.IndexOf(")", braceIndex + 1, StringComparison.Ordinal);

                    if (closingBraceIndex == -1)
                    {
                        break;
                    }

                    valueString = lineString.Substring(braceIndex + 1, closingBraceIndex - braceIndex - 1);

                    index = closingBraceIndex + 1;

                    matchCounter++;
                } while (index != -1 && (matchCounter-1) != matchIndex);

                return Evaluate(valueString);
            }

            return 0;
        }
        
        // filename, line, call_count
        private static readonly Dictionary<string, Dictionary<int, List<int>>> map = new Dictionary<string, Dictionary<int, List<int>>>();
        
        public static int GetCallCount(int frameIndex = 3)
        {
            var info = TweakAS.GetFileLineColumnInfo(frameIndex);

            var filename = info.Item1;
            var line = info.Item2;

            var offset = info.Item4;

            if (map.ContainsKey(filename))
            {
                var lineInfo = map[filename];

                if (lineInfo.ContainsKey(line))
                {
                    int index = lineInfo[line].IndexOf(offset);

                    if (index >= 0)
                    {
                        return index;
                    }

                    lineInfo[line].Add(offset);

                    return 0;
                }
                
                lineInfo.Add(line, new List<int>() {offset});

                return 0;
            }
            
            map.Add(filename, new Dictionary<int, List<int>> { { line, new List<int>() { offset} } });
            
            return 0;
        }
    }
}

// ReSharper disable once CheckNamespace