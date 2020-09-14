using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace TweakAsSharp
{
    public static class Benchmark
    {
        public static void RunEvalBenchmark(int iterationCount = 100)
        {
            float sum = 0, min = float.MaxValue, max = float.MinValue;

            iterationCount.Times((i) =>
            {
                var timer = new Stopwatch();
                timer.Start();

                var value = TweakAS.Evaluate("" + i); // hope that optimizer won't take this away

                timer.Stop();

                var elapsed = timer.ElapsedMilliseconds;

                sum += elapsed;

                if (elapsed < min)
                    min = elapsed;

                if (elapsed > max)
                    max = elapsed;
            });
            
            Console.WriteLine($"Code Eval runtime: Min: {min}, Max: {max}, Mean: {sum/iterationCount}");
        }
    }
}