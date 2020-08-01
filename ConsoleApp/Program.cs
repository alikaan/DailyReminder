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
        static void Main(string[] args)
        {
            DailyReminder.Reminder _reminder = new DailyReminder.Reminder("05542985313");
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

        }
    }
}
