using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Krafi.DataObjects
{
    public class Schedule : ISchedule
    {
        private LocationIdMap<List<TimeSpan>> _times;

        public Schedule()
        {
            _times = new LocationIdMap<List<TimeSpan>>();
        }

        public void InsertTime(string locationId, List<TimeSpan> times) 
        {
            _times.TryAdd(locationId, times);
        }

        public TimeSpan GetClosestDepartureTime(string locationId, TimeSpan time) 
        {
            if(!_times.ContainsKey(locationId))
                throw new Exception("Given stop doesn't exist in this schedule");

            foreach(var stopTime in _times[locationId])
                if(stopTime >= time)
                    return stopTime;

            // If no later time has been found, it means that there are no transports coming this day.
            // Thus return the first time available the following day.
            return new TimeSpan(1, 0, 0, 0) + _times[locationId][0];
        }
    }
}