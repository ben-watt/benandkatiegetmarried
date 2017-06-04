using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public EventTypes Type { get; set; }

        public void SetDates(DateTime startTime, DateTime endTime)
        {
            if(startTime < DateTime.Now)
            {
                throw new ArgumentException("Cannot have a start date before todays date");
            }
            if(startTime > endTime)
            {
                throw new ArgumentException("Cannot have an end date before the start date");
            }
            StartTime = startTime;
            EndTime = endTime;
        }
    }

    public enum EventTypes
    {
        Wedding
    }
}
