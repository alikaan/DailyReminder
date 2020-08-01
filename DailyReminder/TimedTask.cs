using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReminder
{
    public enum TaskType
    {
        Instant, Repetitive
    };
    public class TimedTask
    {
        private string _name { get; set; }
        private string _message { get; set; }
        private TaskType _type { get; set; }
        private DateTime _date { get; set; }
        private bool _active { get; set; }
        private int _interval { get; set; }

        public TimedTask(string name, string message, int interval, TaskType type)
        {
            _name = name;
            _message = message;
            _interval = interval;
            _type = type;
            _active = true;
            _date = LocalTime.Get().AddHours(_interval);
        }
        public TimedTask(string name, string message, int interval, TaskType type, DateTime startDate)
        {
            _name = name;
            _message = message;
            _interval = interval;
            _type = type;
            _active = true;
            if(DateTime.Compare(LocalTime.Get(), startDate) > 0)
            {
                _date = startDate.AddHours(interval);
            }
            else
            {
                _date = startDate;
            }
        }

        public string GetMessage()
        {
            return _message;
        }
        public string GetName()
        {
            return _name;
        }
        public string GetTime()
        {
            return _date.ToString();
        }

        public TaskType Type()
        {
            return _type;
        }
        public bool IsActive()
        {
            return _active;
        }

        public void SetAsPassive()
        {
            _active = false;
        }
        public bool IsTaskReady()
        {
            var result = DateTime.Compare(LocalTime.Get(), _date);
            if(result > 0)
            {
                _date = LocalTime.Get().AddHours(_interval);
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}
