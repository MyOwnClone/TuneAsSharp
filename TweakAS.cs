using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace TuneAsSharp
{
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once ArrangeTypeModifiers
    public static class TweakAS
    {
        // not thread safe I guess
        private const string TokenToLookFor = "t" + "v";

        // filename, line, call_count
        private static readonly Dictionary<string, Dictionary<int, List<int>>> LineMap =
            new Dictionary<string, Dictionary<int, List<int>>>();

        // filename, line, column, ilOffset
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

        // ReSharper disable once InconsistentNaming
        public static object tv(object defaultValue)
        {
            var value = ResolveValue(GetFileLineColumnInfo(2), defaultValue, GetCallCount());

            return value;
        }
        
        public static int tv(int defaultValue)
        {
            var value = (int) ResolveValue(GetFileLineColumnInfo(2), defaultValue, GetCallCount());

            return value;
        }
        
        public static float tv(float defaultValue)
        {
            var value = (float) ResolveValue(GetFileLineColumnInfo(2), defaultValue, GetCallCount());

            return value;
        }
        
        public static string tv(string defaultValue)
        {
            var value = (string) ResolveValue(GetFileLineColumnInfo(2), defaultValue, GetCallCount());

            return value;
        }

        private static object ResolveValue(Tuple<string, int, int, int> info, object defaultValue, int matchIndex = 0)
        {
            var (filename, lineNumber, columnNumber, _) = info;

            string[] allLines;

            try
            {
                allLines = File.ReadAllLines(filename);
            }
            catch (IOException)
            {
                return defaultValue;
            }

            if (allLines == null)
            {
                return defaultValue;
            }

            if (allLines.Length == 0)
            {
                return defaultValue;
            }

            var lineString = allLines[lineNumber - 1];

            if (!lineString.Contains(TokenToLookFor))
                return 0;

            var index = columnNumber;
            var valueString = string.Empty;

            var matchCounter = 0;

            do
            {
                index = lineString.IndexOf(TokenToLookFor, index, StringComparison.Ordinal);

                if (index == -1)
                {
                    break;
                }

                var lengthToSkip = TokenToLookFor.Length;

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
            } while ((matchCounter - 1) != matchIndex);

            return Parser.Evaluate(valueString);
        }

        private static int GetCallCount(int frameIndex = 3)
        {
            var (filename, line, _, offset) = TweakAS.GetFileLineColumnInfo(frameIndex);

            if (LineMap.ContainsKey(filename))
            {
                var lineInfo = LineMap[filename];

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

            LineMap.Add(filename, new Dictionary<int, List<int>>
            {
                {
                    line, new List<int>
                    {
                        offset
                    }
                }
            });

            return 0;
        }
    }
}

// ReSharper disable once CheckNamespace