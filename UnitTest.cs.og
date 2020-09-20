using System;

namespace TuneAsSharp
{
    public class UnitTest
    {
        public static void TestFunctionNoRoslyn()
        {
            TestFunction(false);
        }

        public static void TestFunctionRoslyn()
        {
            TestFunction(true);
        }

        public static void TestFunction(bool useRoslyn = false)
        {
            Parser.UseRoslyn = useRoslyn;

            while (true)
            {
                var condition1 = TweakAS.tv("empty");
                
                if (condition1.Equals("test"))
                {
                    Console.WriteLine("Someone touched my tralala!");
                    
                    return;
                }

                Console.WriteLine("Nobody touched my tralala :-(");
            }
        }
    }
}