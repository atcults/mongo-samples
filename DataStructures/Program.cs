using System;

namespace DataStructures
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            await ConcurrentDictionaryProgram.Run();

            Console.WriteLine("DONE");
        }
    }
}