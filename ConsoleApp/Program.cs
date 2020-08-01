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
            Console.WriteLine(_reminder.GetCurrentDate());
            Console.ReadLine();

        }
    }
}
