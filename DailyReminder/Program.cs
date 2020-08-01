using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReminder
{
    class Program
    {
        static void Main(string[] args)
        {
            NotificationService.SMSService _smsService = new NotificationService.SMSService();

            var result = _smsService.SendSMS("05542985313", "dailyReminder", "goog morning");

            if (result)
            {
                Console.Write("mesaj basariyla gonderildi!");
            }
            else
            {
                Console.Write("mesaj gonderilemedi!");
            }
            Console.ReadLine();
        }
    }
}
