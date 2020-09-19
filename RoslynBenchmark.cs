using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace TweakAsSharp
{
    public static class RoslynBenchmark
    {
        public static void Run(int iterationCount = 100)
        {
            float sum = 0, min = float.MaxValue, max = float.MinValue;

            var originalSetting = Parser.UseRoslyn;

            Parser.UseRoslyn = true;

            iterationCount.Times((i) =>
            {
                var timer = new Stopwatch();
                timer.Start();

                // ReSharper disable once UnusedVariable
                var value = Parser.Evaluate("" + i); // hope that optimizer won't take this away

                timer.Stop();

                var elapsed = timer.ElapsedMilliseconds;

                sum += elapsed;

                if (elapsed < min)
                    min = elapsed;

                if (elapsed > max)
                    max = elapsed;
            });

            Parser.UseRoslyn = originalSetting;
            
            Console.WriteLine($"Code Eval runtime: Min: {min}, Max: {max}, Mean: {sum/iterationCount}");
        }
    }
}