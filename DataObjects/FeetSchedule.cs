using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Krafi.DataObjects
{
    public class FeetSchedule : ISchedule
    {
        // We don't need to save any times for traveling on feet
        public void InsertTime(string stopName, List<TimeSpan> times) {}

        // A person can depart at any time
        public TimeSpan GetClosestDepartureTime(string stopName, TimeSpan time) 
        {
            return time;
        }
    }
}