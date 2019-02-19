using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Krafi.DataObjects
{
    public class Schedule : ISchedule
    {
        private Dictionary<string, List<TimeSpan>> _times = new Dictionary<string, List<TimeSpan>>();

        public void InsertTime(string stopId, List<TimeSpan> times) 
        {
            _times.TryAdd(stopId, times);
        }

        public TimeSpan GetClosestDepartureTime(string stopId, TimeSpan time) 
        {
            if(!_times.ContainsKey(stopId))
                throw new Exception("Given stop doesn't exist in this schedule");

            foreach(var stopTime in _times[stopId])
                if(stopTime >= time)
                    return stopTime;

            // If no later time has been found, it means that there are no transports coming this day.
            // Thus return the first time available the following day.
            return new TimeSpan(1, 0, 0, 0) + _times[stopId][0];
        }
    }
}