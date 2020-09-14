using System;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace TweakAsSharp
{
    static class TweakAS
    {
        public static Tuple<string, int> GetFileLineInfo(int frameIndex = 1)
        {
            var st = new StackTrace(true);

            var frame = st.GetFrame(frameIndex);

            if (frame != null)
            {
                var filename = frame.GetFileName();
                var line = frame.GetFileLineNumber();
        
                return new Tuple<string, int>(filename, line);
            }

            return null;
        }

        static TweakAS()
        {
            Console.Write("Warming up the runtime...");
            Evaluate("42");    // warm up
            Console.WriteLine(" Done");
        }
    
        // not thread safe I guess
        private static Script<object> _mBaseScript = CSharpScript.Create("");

        // not thread safe I guess
        public static object Evaluate(string code)
        {
            return _mBaseScript.ContinueWith(code).RunAsync().Result.ReturnValue;
        }
    }
}

// ReSharper disable once CheckNamespace