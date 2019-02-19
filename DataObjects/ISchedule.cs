using System;
using System.Collections.Generic;

namespace Krafi.DataObjects
{
    public interface ISchedule
    {
        void InsertTime(string stopId, List<TimeSpan> time);
        TimeSpan GetClosestDepartureTime(string stopId, TimeSpan time);
    }
}