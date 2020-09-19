using System;

// ReSharper disable once CheckNamespace
namespace TweakAsSharp
{
    internal static class Program
    {
        private static void Main()
        {
            //Benchmark.RunEvalBenchmark();

            Parser.UseRoslyn = false;

            var counter = 0;
            
            while (true)
            {
                Console.WriteLine( counter + " " + TweakAS.tv("test") + " " + TweakAS.tv("moarolka"));
                
                //Console.WriteLine( counter + " " + TweakAS.tv("gloryhole")); 
                //Console.WriteLine( counter + " " + TweakAS.tv("testxyz"));
                
                Console.WriteLine(OtherModule.MethodWithTweakableValue());

                counter++;
            }
        }
    }
}