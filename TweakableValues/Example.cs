using System;
using TuneAsSharp;

// ReSharper disable once CheckNamespace
namespace TweakableValues
{
    internal static class Example
    {
        private static void Main()
        {
            // simpler literals can be parsed without Roslyn, we'll need it only for actual code (tweakable functions)
            Parser.UseRoslyn = false; 

            var counter = 0;

            while (true)
            {
                Console.WriteLine( counter + " " + Tune.tv("test") + " " + Tune.tv("moarolka"));

                Console.WriteLine( counter + " " + Tune.tv("gloryhole")); 
                Console.WriteLine( counter + " " + Tune.tv("testxyz"));

                Console.WriteLine(OtherModule.MethodWithTweakableValue());

                counter++;
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}