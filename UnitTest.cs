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
                var condition3 = TweakAS.tv(0.0f); var condition4 = TweakAS.tv(0);
                
                var condition5 = OtherModuleUnitTest.MethodWithTweakableValue();

                const double tolerance = 0.01f;
                
                if (condition1.Equals("test") && condition2.Equals("moarolka") && Math.Abs((float) condition3 - 3.14f) < tolerance &&
                    (int) condition4 == 42 && (int) condition5 == 555)
                {
                    return;
                }
            }
        }
    }
}