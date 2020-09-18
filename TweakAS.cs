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
        private static readonly Script<object> baseScript = CSharpScript.Create("");
        private const string tokenToLookFor = "t" + "v";
        
        
        // filename, line, call_count
        private static readonly Dictionary<string, Dictionary<int, List<int>>> lineMap = new Dictionary<string, Dictionary<int, List<int>>>();

        static TweakAS()
        {
            Console.Write("Warming up the runtime...");
            Evaluate("42");    // warm up
            Console.WriteLine(" Done\n=======");
        }

        private static Tuple<string, int, int, int> GetFileLineColumnInfo(int frameIndex = 1)
        {
            var st = new StackTrace(true);

            var frame = st.GetFrame(frameIndex);

            if (frame == null) 
                return null;
            
            var filename = frame.GetFileName();
            var line = frame.GetFileLineNumber();
            var column = frame.GetFileColumnNumber();

            var ilOffset = frame.GetILOffset();
        
            return new Tuple<string, int, int, int>(filename, line, column, ilOffset);

        }

        // not thread safe I guess
        public static object Evaluate(string code)
        {
            return baseScript.ContinueWith(code).RunAsync().Result.ReturnValue;
        }

        public static object tv(object defaultValue)
        {
            var value = ResolveValue(GetFileLineColumnInfo(2), defaultValue, GetCallCount());

            return value;
        }

        private static object ResolveValue(Tuple<string,int,int,int> info, object defaultValue, int matchIndex = 0)
        {
            var (filename, lineNumber, columnNumber, _) = info;

            var allLines = System.IO.File.ReadAllLines(filename);

            if (allLines.Length == 0)
            {
                return defaultValue;
            }

            var lineString = allLines[lineNumber - 1];

            if (!lineString.Contains(tokenToLookFor)) 
                return 0;
            
            var index = columnNumber;
            var valueString = string.Empty;

            var matchCounter = 0;

            do
            {
                index = lineString.IndexOf(tokenToLookFor, index, StringComparison.Ordinal);

                if (index == -1)
                {
                    break;
                }

                var lengthToSkip = tokenToLookFor.Length;

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
            } while ((matchCounter-1) != matchIndex);

            return Evaluate(valueString);

        }

        private static int GetCallCount(int frameIndex = 3)
        {
            var (filename, line, _, offset) = TweakAS.GetFileLineColumnInfo(frameIndex);

            if (lineMap.ContainsKey(filename))
            {
                var lineInfo = lineMap[filename];

                if (lineInfo.ContainsKey(line))
                {
                    var index = lineInfo[line].IndexOf(offset);

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
            
            lineMap.Add(filename, new Dictionary<int, List<int>> { { line, new List<int>
            {
                    offset
                } } });
            
            return 0;
        }
    }
}

// ReSharper disable once CheckNamespace