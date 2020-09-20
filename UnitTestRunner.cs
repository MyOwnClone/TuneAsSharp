using System;
using System.IO;
using System.Threading;

namespace TuneAsSharp
{
    public static class UnitTestRunner
    {
        public static bool Run(bool useRoslyn)
        {
            Thread thread = null;
            
            thread = useRoslyn ? new Thread(UnitTest.TestFunctionRoslyn) : new Thread(UnitTest.TestFunctionNoRoslyn);
            
            thread.Start();
            
            Thread.Sleep(1000);
            
            TriggerChange();

            var properExit = thread.Join(10000000);

            if (!properExit)
            {
                string no = "No";
                
                Console.WriteLine($"{(useRoslyn ? string.Empty : no )}Roslyn Test FAILED!");
                
                Clean();
                
                Environment.Exit(1);
            }
            
            Clean();

            return properExit;
        }

        static void RepeatUntilNoException(Action action)
        {
            while (true)
            {
                try
                {
                    action();
                }
                catch (IOException)
                {
                    continue;   
                }

                break;
            }
        }

        public static void TriggerChange()
        {
            RepeatUntilNoException(() => File.Copy("UnitTest.cs.new", "UnitTest.cs", true));
            RepeatUntilNoException(() => System.IO.File.Copy("OtherModuleUnitTest.cs.new", "OtherModuleUnitTest.cs", true));
        }

        public static void Clean()
        {
            RepeatUntilNoException(() => File.Copy("UnitTest.cs.og", "UnitTest.cs", true));
            RepeatUntilNoException(() => File.Copy("OtherModuleUnitTest.cs.og", "OtherModuleUnitTest.cs", true));
        }
    }
}