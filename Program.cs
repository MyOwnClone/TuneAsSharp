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
                Console.WriteLine( counter + " " + TweakAS.tv("tests"));

                counter++;
            }
        }
    }
}