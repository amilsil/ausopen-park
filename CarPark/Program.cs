using CarPark.Config;
using Microsoft.Practices.Unity;
using System;

namespace CarPark
{
    class Program
    {
        static void Main(string[] args)
        {
            /**
             * This input date format can be improved
             * either by a GUI or a human friendly format.
             * But sticking to this to keep this simple.
             */

            Console.Write("Enter the EntryTime(in format 2017-01-01T10:00:00):");
            var entryTimeStr = Console.ReadLine();

            Console.Write("Enter the ExitTime(in format 2017-01-01T10:00:00):");
            var exitTimeStr = Console.ReadLine();

            // Proceed only if the times entered are valid.
            DateTime entryTime, exitTime;
            if (!DateTime.TryParse(entryTimeStr, out entryTime)
                || !DateTime.TryParse(exitTimeStr, out exitTime))
            {
                Console.WriteLine("Please enter the times in the given format.");
                Console.ReadKey();
                return;
            }

            var container = UnityConfig.RegisterComponents();
            var rateCalculator = container.Resolve<IRateCalculator>();
            var calculatedRate = rateCalculator.GetRate(entryTime, exitTime);

            Console.WriteLine($"Your parking charge is ${calculatedRate.Rate} on a {calculatedRate.RateType} rate");
            Console.ReadKey();
        }
    }
}
