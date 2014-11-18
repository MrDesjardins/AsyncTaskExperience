using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAsyncTest
{
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {

            Task.Run(async() =>
            {
                var allStrings = new List<Task<string>>();
                Console.WriteLine("Start");
                Console.WriteLine("Add First");
                allStrings.Add(GiveMeAStringAsync("First", 1000));
                Console.WriteLine("Add Second");
                allStrings.Add(GiveMeAStringAsync("Second", 500));
                //Thread.Sleep(5000);
                Console.WriteLine("Add Third");
                allStrings.Add(GiveMeAStringAsync("Third", 3000));
                string thirdTask = "Default Third String";
                thirdTask = await allStrings.Last();
                Console.WriteLine("Add Forth");
                allStrings.Add(GiveMeAStringAsync("Forth" + thirdTask, 200));

                Console.WriteLine("Wait All");
                var task = allStrings.ToList();
                var allAwaitedTask = await Task.WhenAll(task);
                foreach (var s in allAwaitedTask)
                {
                    Console.WriteLine("->" + s);
                }
                Console.WriteLine("Finish");

            }).Wait();
            Console.WriteLine("Press Any Keys to stop");
            Console.ReadLine();
        }

        private static async Task<string> GiveMeAStringAsync(string suffixe, int millisecondTaskTake)
        {
            Console.WriteLine("Begin GiveMeAString:" + suffixe +" for " + millisecondTaskTake);

            await Task.Delay(millisecondTaskTake);

            Console.WriteLine("End GiveMeAString:" + suffixe + " for " + millisecondTaskTake);
            return "GiveMeString execute " + suffixe;
        }
    }

    
}
