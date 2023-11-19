using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planer
{
    internal class Plan : SmallerEvent
    {
        public string EventName;
        public DateTime Date;
        public int Priority;
        public string Category;
        public bool IsEventFinished;
        public Plan() 
        {
            EventName = string.Empty;
            Date = DateTime.Now;
            Priority = 0;
            Category = string.Empty;
            IsEventFinished = false;
        }
        public Plan(string eventName, DateTime dateTime, int priority, string category)
        {
            EventName = eventName;
            Date = dateTime;
            Priority = priority;
            Category = category;
            IsEventFinished = false;
        }
    }

    internal class SmallerEvent
    {
        string EventName;
        DateTime Date;
        int Priority;

        public SmallerEvent()
        {
            EventName= string.Empty;
            Date = DateTime.Now;
            Priority = 0;
        }
    }
}
