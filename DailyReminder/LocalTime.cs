using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReminder
{
    public class LocalTime
    {
        public static DateTime Get()
        {
            var time = DateTime.UtcNow;
            time = time.AddHours(3);
            return time;
        }
    }
}
