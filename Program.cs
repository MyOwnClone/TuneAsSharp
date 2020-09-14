using System;

// ReSharper disable once CheckNamespace
namespace TweakAsSharp
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine(TweakAS.GetFileLineInfo());
            Benchmark.RunEvalBenchmark();
        }
    }
}