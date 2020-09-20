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

                var condition3 = (float) TweakAS.tv(0.0f);

                var condition4 = (int) TweakAS.tv(0);

                const double tolerance = 1e-2;
                
                if (condition1.Equals("test") && condition2.Equals("moarolca") && Math.Abs(condition3 - 3.14f) < tolerance && condition4 == 42)
                {
                    Console.WriteLine("Someone touched my tralala!");
                    
                    return;
                }

                Console.WriteLine("Nobody touched my tralala :-(");
            }
        }
    }
}