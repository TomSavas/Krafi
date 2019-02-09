using System;
using System.Collections.Generic;

namespace Krafi.DataObjects
{
    public interface ISchedule : IEnumerable<DateTime>
    {
        DateTime GetClosestTime(DateTime time);
        void InsertTime(DateTime time);
    }
}