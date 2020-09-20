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
                var condition1 = TweakAS.tv("empty"); var condition2 = TweakAS.tv("empty2");
                
                if (condition1.Equals("test") && condition2.Equals("moarolca"))
                {
                    Console.WriteLine("Someone touched my tralala!");
                    
                    return;
                }

                Console.WriteLine("Nobody touched my tralala :-(");
            }
        }
    }
}