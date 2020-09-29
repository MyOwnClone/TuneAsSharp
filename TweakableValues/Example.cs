using System;
using TuneAsSharp;

// ReSharper disable once CheckNamespace
namespace TweakableValues
{
    internal static class Example
    {
        // 1. start your app as usual
        private static void Main()
        {
            // simpler literals can be parsed without Roslyn, we'll need it only for actual code (tweakable functions)
            Parser.UseRoslyn = false; 

            var counter = 0;

            while (true)
            {
                // 2. change any value in Tune.tv() calls (in editor while the app is already running) and see that printed values in running app change
                
                Console.WriteLine( counter + " " + Tune.tv("test") + " " + Tune.tv("moarolka"));

                Console.WriteLine( counter + " " + Tune.tv("gloryhole")); 
                Console.WriteLine( counter + " " + Tune.tv("testxyz"));

                // 3. it works even for different source files, so you can edit the Tune.tv() parameters even in other modules
                Console.WriteLine(OtherModule.MethodWithTweakableValue());

                counter++;
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}