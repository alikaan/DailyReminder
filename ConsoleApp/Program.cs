using DailyReminder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static int Main(string[] args)
        {
            string _phoneNo;
            if (args.Length == 1 && args[0].Length == 11)
            {
                _phoneNo = args[0];
            }
            else
            {
                Console.WriteLine("Phone number is wrong! Please Try Again. ( 05xx xxx xx xx )");
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
                return 0;
            }
            DailyReminder.Reminder _reminder = new DailyReminder.Reminder(_phoneNo);
            Console.WriteLine("Current time is : " + LocalTime.Get());

            var dateNow = DateTime.Now;
            var morningDate = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 8, 0, 0);
            var nightDate = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 23, 0, 0);
            
            _reminder.AddTask("morning", "Good Morning Babe!", 24, TaskType.Repetitive, morningDate);

            _reminder.AddTask("night", "Good Night Babe!", 24, TaskType.Repetitive, nightDate);

            _reminder.AddTask("water", "Hey babe, It is time to drink water!", 3, TaskType.Repetitive);

            Console.WriteLine("Daily reminder has started!");
            _reminder.Start();

            Console.ReadLine();
            return 0;
        }
    }
}
