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
        public List<SmallerEvent> SmallerEvents;
        public Plan() 
        {
            EventName = string.Empty;
            Date = DateTime.Now;
            Priority = 0;
            Category = string.Empty;
            IsEventFinished = false;
            SmallerEvents = new List<SmallerEvent>();
        }
        public Plan(string eventName, int priority, string category)
        {
            EventName = eventName;
            Date = DateTime.Now;
            Priority = priority;
            Category = category;
            IsEventFinished = false;
            SmallerEvents = new List<SmallerEvent>();
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
