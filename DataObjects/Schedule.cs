using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Krafi.DataObjects
{
    public class Schedule : ISchedule
    {
        private LocationIdMap<List<TimeSpan>> _times;
        private LocationIdMap<List<KeyValuePair<int, TimeSpan>>> _departingBusNumbersWithTimes;

        public Schedule()
        {
            _times = new LocationIdMap<List<TimeSpan>>();
            _departingBusNumbersWithTimes = new LocationIdMap<List<KeyValuePair<int, TimeSpan>>>();
        }

        public void InsertTime(string locationId, List<TimeSpan> times) 
        {
            _times.TryAdd(locationId, times);
        }

        public void InsertTime(string locationId, List<KeyValuePair<int, TimeSpan>> departingBusNumbersWithTimes) 
        {
            _departingBusNumbersWithTimes.TryAdd(locationId, departingBusNumbersWithTimes);
            _times.TryAdd(locationId, departingBusNumbersWithTimes.Select(x => x.Value).ToList());
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

        public TimeSpan GetTravelTime(string startLocationId, string endLocationId, TimeSpan time)
        {
            if(!_departingBusNumbersWithTimes.ContainsKey(startLocationId) || !_departingBusNumbersWithTimes.ContainsKey(endLocationId))
                throw new Exception("Given stop doesn't exist in this schedule");

            TimeSpan departureTime = new TimeSpan();
            int busNumber = -1;
            foreach(var valueBundle in _departingBusNumbersWithTimes[startLocationId])
            {
                if(valueBundle.Value >= time)
                {
                    busNumber = valueBundle.Key;
                    departureTime = valueBundle.Value;
                    break;
                }
            }

            TimeSpan arrivalTime = TimeSpan.MaxValue;
            foreach(var valueBundle in _departingBusNumbersWithTimes[endLocationId])
            {
                if(valueBundle.Key == busNumber && valueBundle.Value >= departureTime)
                {
                    arrivalTime = valueBundle.Value;
                }
            }

            if(busNumber == -1)
                throw new Exception("Fuck me " + time);

            return arrivalTime.Subtract(departureTime);
        }
    }
}