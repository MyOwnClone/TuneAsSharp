﻿using System;
using TuneAsSharp;

// ReSharper disable once CheckNamespace
namespace TweakableValues
{
    internal static class TweakableValues
    {
        private static void Main()
        {
            //RoslynBenchmark.Run();

            Parser.UseRoslyn =
                false; // simpler literals can be parsed without Roslyn, we'll need it only for actual code (tweakable functions)

            var counter = 0;

            while (true)
            {
                Console.WriteLine(counter + " " + TweakAS.tv("test") + " " + TweakAS.tv("moarolka"));

                Console.WriteLine( counter + " " + TweakAS.tv("gloryhole")); 
                Console.WriteLine( counter + " " + TweakAS.tv("testxyz"));

                Console.WriteLine(OtherModule.MethodWithTweakableValue());

                counter++;
            }
        }
    }
}