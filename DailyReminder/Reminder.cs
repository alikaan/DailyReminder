using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReminder
{
    public class Reminder
    {
        private NotificationService.SMSService _smsService { get; set; }
        private string _phoneNumber { get; set; }
        private DateTime _dateTime { get; set; }
        private List<TimedTask> _timedTasks { get; set; }

        public Reminder(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
            _smsService = new NotificationService.SMSService();
            _dateTime = LocalTime.Get();
            _timedTasks = new List<TimedTask>();
        }

        public void AddTask(string name, string message, int interval, TaskType type)
        {
            var task = new TimedTask(name, message, interval, type);
            _timedTasks.Add(task);
        }
        public void AddTask(string name, string message, int interval, TaskType type, DateTime startDate)
        {
            var task = new TimedTask(name, message, interval, type, startDate);
            _timedTasks.Add(task);
        }

        public void Start()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var startMessage = new StringBuilder();
            startMessage.AppendLine("Welcome to daily reminder service by Ali Kaan Türkmen.");
            startMessage.Append("Tasks:");
            foreach(var task in _timedTasks)
            {
                startMessage.Append(task.GetName() + ",");
            }
            startMessage.AppendLine();
            Console.WriteLine(startMessage.ToString());
            SendMessage(startMessage.ToString());
            while(true)
            {
                while (sw.Elapsed > TimeSpan.FromMinutes(10))
                {
                    Console.WriteLine("Checking tasks...");
                    Console.WriteLine("Current time is : " + LocalTime.Get());
                    Console.WriteLine("---------------------------------------------");
                    foreach (var task in _timedTasks)
                    {
                        //Console.WriteLine("---------------------------------------------");
                        //Console.WriteLine("Task Name: " + task.GetName());
                        //Console.WriteLine("Task Message: " + task.GetMessage());
                        //Console.WriteLine("Task Time: " + task.GetTime());
                        //Console.WriteLine("---------------------------------------------");
                        if (task.IsTaskReady() && task.IsActive())
                        {
                            Console.WriteLine("\"" + task.GetName() + "\"" + "task is ready to execute...");
                            if (SendTaskMessage(task))
                            {
                                Console.WriteLine("Task executed successfully!");
                                if (task.Type() == TaskType.Instant)
                                {
                                    Console.WriteLine("\"" + task.GetName() + "\"" + "task state ise passive now!");
                                    task.SetAsPassive();
                                }                             
                            }
                            else
                            {
                                Console.WriteLine("Task execution failed!");
                            }
                        }
                    }
                    sw.Restart();
                }
            }
        }

        private bool SendTaskMessage(TimedTask task)
        {
            return _smsService.SendSMS(_phoneNumber, "dailyReminder", task.GetMessage());
        }
        private bool SendMessage(string message)
        {
            return _smsService.SendSMS(_phoneNumber, "dailyReminder", message);
        }
    }
}
