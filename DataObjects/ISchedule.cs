using System;
using System.Collections.Generic;

namespace Krafi.DataObjects
{
    public interface ISchedule : IEnumerable<TimeSpan>
    {
        TimeSpan GetClosestTime(string stopName, TimeSpan time);
        void InsertTime(string stopName, List<TimeSpan> time);
    }
}