using System;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace tweak_as_sharp
{
    class Program
    {
        static int iterations = 100;

        static void TestIterations()
        {
            var code = "42";

            var test0_ms = Test(1, () => CSharpScript.RunAsync(code).Wait(), warm: false);
            Console.WriteLine($"{test0_ms} : Run first script.");

            var test1_ms = Test(iterations, () => CSharpScript.RunAsync(code).Wait(), warm: true);
            Console.WriteLine($"{test1_ms} : Run new script (warm).");

            var baseScript = CSharpScript.Create("");
            var test3_ms = Test(iterations, () => baseScript.ContinueWith(code).RunAsync().Wait(), warm: true);
            Console.WriteLine($"{test3_ms} : Run separate script continued new from shared script (warm).");

            var test2Script = CSharpScript.Create(code);
            var test2_ms = Test(iterations, () => test2Script.RunAsync().Wait(), warm: true);
            Console.WriteLine($"{test2_ms} : Run same script over and over again (warm).");
        }

        static long Test(int iterations, Action testAction, bool warm)
        {
            if (warm)
            {
                testAction();
            }

            var timer = new Stopwatch();
            timer.Start();

            for (int i = 0; i < iterations; i++)
            {
                testAction();
            }

            timer.Stop();

            return timer.ElapsedMilliseconds / iterations;
        }
        
        static void Main(string[] args)
        {
            /*StackTrace st = new StackTrace(new StackFrame(true));

            var frame = st.GetFrame(0);

            var filename = frame.GetFileName();
            var line = frame.GetFileLineNumber();
            
            Console.WriteLine($"{filename}, {line}");
            
            var result = CSharpScript.EvaluateAsync("1 + 3").Result;
            
            Console.WriteLine($"{result}");
            
            TestIterations();
            
            var baseScript = CSharpScript.Create("");

            for (int i = 0; i < 100; i++)
            {
                var code = $"{i}";
                
                var timer = new Stopwatch();
                timer.Start();
                
                baseScript.ContinueWith(code).RunAsync().Wait();
                
                timer.Stop();
                
                Console.WriteLine($"{timer.ElapsedMilliseconds}");
            }*/
            
            Console.WriteLine(TweakAS.GetFileLineInfo());

            for (int i = 0; i < 10; i++)
            {
                var timer = new Stopwatch();
                timer.Start();

                var value = TweakAS.Evaluate(""+i);
                
                timer.Stop();
                
                Console.WriteLine($"{timer.ElapsedMilliseconds}ms");
                
                //Console.WriteLine(value);
            }
        }
    }
}