using System;
using System.Threading;

namespace TuneAsSharp
{
    public static class UnitTestRunner
    {
        public static bool Run()
        {
            var thread = new Thread(UnitTest.TestFunctionNoRoslyn);
            thread.Start();
            
            Thread.Sleep(100);
            
            TriggerChange();

            var properExit = thread.Join(10000000);

            if (!properExit)
            {
                Console.WriteLine("Test FAILED!");
                
                Clean();
                
                Environment.Exit(1);
            }
            
            Clean();

            return properExit;
        }

        public static void TriggerChange()
        {
            System.IO.File.Copy("UnitTest.cs.new", "UnitTest.cs", true);
            System.IO.File.Copy("OtherModuleUnitTest.cs.new", "OtherModuleUnitTest.cs", true);
        }

        public static void Clean()
        {
            System.IO.File.Copy("UnitTest.cs.og", "UnitTest.cs", true);
            System.IO.File.Copy("OtherModuleUnitTest.cs.og", "OtherModuleUnitTest.cs", true);
        }
    }
}