using System;
using System.Collections.Generic;

namespace TweakAsSharp
{
    public class CallPerLineCountTest
    {
        // filename, line, call_count
        private static readonly Dictionary<string, Dictionary<int, int>> map = new Dictionary<string, Dictionary<int, int>>();

        private static int oldLine = -1;
        
        public static int GetCallCount()
        {
            var info = TweakAS.GetFileLineColumnInfo(2);

            var filename = info.Item1;
            var line = info.Item2;

            if (map.ContainsKey(filename))
            {
                var lineInfo = map[filename];

                if (line != oldLine)
                {
                    lineInfo.Clear();
                }

                if (lineInfo.ContainsKey(line))
                {
                    oldLine = line;
                    
                    return lineInfo[line]++;
                }
                
                lineInfo.Add(line, 1);

                oldLine = line;

                return 0;
            }
            
            map.Add(filename, new Dictionary<int, int> { { line, 1 } });

            oldLine = line;
            
            return 0;
        }
    }
}