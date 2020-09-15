using System;

// ReSharper disable once CheckNamespace
namespace TweakAsSharp
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine(TweakAS.GetFileLineColumnInfo());   Console.WriteLine(TweakAS.GetFileLineColumnInfo());
            Benchmark.RunEvalBenchmark();

            int counter = 0;
            
            while (true)
            {
                //Console.WriteLine( counter + " " + TweakAS.tv("test") + " " + TweakAS.tv("moar")); // two tv's won't work yet
                
                Console.WriteLine( counter + " " + TweakAS.tv("test")); 
                Console.WriteLine( counter + " " + TweakAS.tv("test"));

                counter++;
            }
        }
    }
}