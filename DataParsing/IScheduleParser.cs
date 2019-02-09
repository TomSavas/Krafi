using System.Collections.Generic;
using Krafi.DataObjects;

namespace Krafi.DataParsing
{
    public interface IScheduleParser 
    {
        List<ISchedule> ParseSchedules();
    }
}