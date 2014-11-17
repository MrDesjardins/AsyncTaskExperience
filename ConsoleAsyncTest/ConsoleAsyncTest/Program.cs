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
                allStrings.Add(GiveMeAString("First", 1000));
                Console.WriteLine("Add Second");
                allStrings.Add(GiveMeAString("Second", 500));

                Console.WriteLine("Add Third");
                allStrings.Add(GiveMeAString("Third", 3000));
                string thirdTask = "Default Third String";
                //thirdTask = await allStrings.Last();
                Console.WriteLine("Add Forth");
                allStrings.Add(GiveMeAString("Forth" + thirdTask, 200));

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

        private static async Task<string> GiveMeAString(string suffixe, int millisecondTaskTake)
        {
            Console.WriteLine("Begin GiveMeAString:" + suffixe +" for " + millisecondTaskTake);

            await Task.Delay(millisecondTaskTake);

            Console.WriteLine("End GiveMeAString:" + suffixe + " for " + millisecondTaskTake);
            return "GiveMeString execute " + suffixe;
        }
    }

    
}
