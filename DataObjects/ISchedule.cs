using System;
using System.Collections.Generic;

namespace Krafi.DataObjects
{
    public interface ISchedule
    {
        void InsertTime(string locationId, List<TimeSpan> time);
        TimeSpan GetClosestDepartureTime(string locationId, TimeSpan time);
    }
}