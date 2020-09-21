using System;
using System.Linq.Expressions;

// ReSharper disable once CheckNamespace
namespace TuneAsSharp
{
    internal static class Program
    {
        private static void Main()
        {
            //RoslynBenchmark.Run();

            /*Parser.UseRoslyn =
                false; // simpler literals can be parsed without Roslyn, we'll need it only for actual code (tweakable functions)

            var counter = 0;

            while (true)
            {
                Console.WriteLine(counter + " " + TweakAS.tv("test") + " " + TweakAS.tv("moarolka"));

                //Console.WriteLine( counter + " " + TweakAS.tv("gloryhole")); 
                //Console.WriteLine( counter + " " + TweakAS.tv("testxyz"));

                Console.WriteLine(OtherModule.MethodWithTweakableValue());

                counter++;
            }*/

            /*Console.WriteLine(UnitTestRunner.Run(false));
            Console.WriteLine(UnitTestRunner.Run(true));*/
            TestTweakableFunctions();
        }

        [TweakableFunction]
        private static int test()
        {
            return 0;
        }
        
        private static object tf(Func<object> action)
        {
            /*
             * the plan is to trigger Parser here, reload __FILE__, look for all [TweakableFunction] functions,
             * load them into the Roslyn and dispatch call to the roslyn context
             * if we are able to call action within the context of code compiled from roslyn, the call to test()
             * should resolve to the one parsed from the reloaded __FILE__.
             * We should also check whether the test() was already parsed from __FILE__, if not, call the action()
             * in original context without roslyn
             * TODO: read https://laurentkempe.com/2019/02/18/dynamically-compile-and-run-code-using-dotNET-Core-3.0/
             */
            
            
            return action();
        }

        private static void TestTweakableFunctions()
        {
            while (true)
            {
                Console.WriteLine(tf(() => test()));
            }
        }
    }
}