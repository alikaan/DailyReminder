using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReminder
{
    public class Reminder
    {
        private NotificationService.SMSService _smsService { get; set; }
        public string _phoneNumber { get; set; }
        public DateTime _dateTime { get; set; }
        public TimeZoneInfo _timeZone { get; set; }

        public Reminder(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
            _smsService = new NotificationService.SMSService();
            _dateTime = new DateTime();
            _dateTime = RefreshTime(_dateTime);
        }

        public void Start()
        {
            while(true)
            {

            }
        }

        public string GetCurrentDate()
        {
            _dateTime = RefreshTime(_dateTime);
            return _dateTime.ToString();
        }

        private DateTime RefreshTime(DateTime time)
        {
            time = DateTime.UtcNow;
            time = time.AddHours(3);
            return time;
        }
    }
}
