using System;
using TuneAsSharp;

namespace TweakableFunctions
{
    internal static class TweakableFunctions
    {
        [TweakableFunction]
        private static int Test()
        {
            return 0;
        }
        
        // ReSharper disable once InconsistentNaming
        private static object tf(Func<object> action)    /* Action<> is not able to return value, Func<> is  */
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

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(tf(() => Test()));
            }
        }
    }
}