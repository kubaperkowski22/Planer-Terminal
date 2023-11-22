using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Planer
{
    internal class Plan
    {
        [JsonPropertyName("EventName")]
        public string EventName { get; set; }
        [JsonPropertyName("Date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("Priority")]
        public int Priority {  get; set; }
        [JsonPropertyName("Category")]
        public string Category { get; set; }
        [JsonPropertyName("IsFinished")]
        public bool IsEventFinished { get; set; }
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
}
