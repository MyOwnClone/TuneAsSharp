using System;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;

// ReSharper disable once CheckNamespace
namespace TweakAsSharp
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine(TweakAS.GetFileLineColumnInfo());   Console.WriteLine(TweakAS.GetFileLineColumnInfo());
            Benchmark.RunEvalBenchmark();
            
            //Func<int, int, int> z = (int a, int b) => a + b;
            
            var result = Task.Run<object>(async () =>
            {
                // CSharpScript.RunAsync can also be generic with typed ReturnValue
                var s = await CSharpScript.RunAsync(@"using System;");

                // continuing with previous evaluation state
                s = await s.ContinueWithAsync(@"var x = ""my/"" + string.Join(""_"", ""a"", ""b"", ""c"") + "".ss"";");
                s = await s.ContinueWithAsync(@"var y = ""my/"" + @x;");
                s = await s.ContinueWithAsync(@"y // this just returns y, note there is NOT trailing semicolon");
                s = await s.ContinueWithAsync(@"Func<int, int, int> z = (int a, int b) => a + b;");
                s = await s.ContinueWithAsync(@"int test(int a, int b) => a + b;");

                s = await s.ContinueWithAsync(@"var test_result = test(42, 0);");

                // inspecting defined variables
                Console.WriteLine("inspecting defined variables:");
                foreach (var variable in s.Variables)
                {
                    Console.WriteLine("name: {0}, type: {1}, value: {2}", variable.Name, variable.Type.Name, variable.Value);

                    if (variable.Type.Name.Contains("Func"))
                    {
                        var value = variable.Value as Func<int, int, int>;

                        var result = value(1, 2);
                        
                        Console.WriteLine($"Result of calling lambda: {result}");
                    }
                }
                
                return s.ReturnValue;
                    
            }).Result;

            //var result = TweakAS.EvaluateLambda("(a, b) => {  return a + b; }");
            
            //Console.WriteLine(result.GetType());

            /*int counter = 0;
            
            while (true)
            {
                //Console.WriteLine( counter + " " + TweakAS.tv("test") + " " + TweakAS.tv("moar")); // TODO: two tv's won't work yet, fix them similar to how it's done in tweak_af, use frame.GetILOffset() to determine order on the line?
                
                Console.WriteLine( counter + " " + TweakAS.tv("test")); 
                Console.WriteLine( counter + " " + TweakAS.tv("test"));

                counter++;
            }*/
        }
    }
}