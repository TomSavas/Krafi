using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Krafi.DataObjects
{
    public class FeetSchedule : ISchedule
    {
        public TimeSpan GetClosestDepartureTime(string stopName, TimeSpan time) 
        {
            return time;
        }

        public void InsertTime(string stopName, List<TimeSpan> times) {}
    }
}