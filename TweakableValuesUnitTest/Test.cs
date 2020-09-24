using System;
using TuneAsSharp;

namespace TweakableValuesUnitTest
{
    internal static class Test
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(UnitTestRunner.Run(false));
            Console.WriteLine(UnitTestRunner.Run(true));
        }
    }
}