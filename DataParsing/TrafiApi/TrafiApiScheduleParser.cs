using System;
using System.Collections.Generic;
using Krafi.DataObjects;

namespace Krafi.DataParsing.TrafiApi
{
    public class TrafiApiScheduleParser
    {
        public ISchedule Parse(TrafiApiTimeJSON timeJSON, TrafiApiDurationJSON durationJSON, List<TrafiApiTimetableStopJSON> stops)
        {
            var schedule = new Schedule();

            var transportStartingTimes = CalculateStartingTimes(timeJSON);
            var durationTimes = CalculateArrivalTimes(durationJSON);

            for(int stopNumber = 0; stopNumber < stops.Count; stopNumber++)
            {
                var times = new List<TimeSpan>();

                for(int departingBusNumber = 0; departingBusNumber < transportStartingTimes.Count; departingBusNumber++)
                    times.Add(transportStartingTimes[departingBusNumber] + durationTimes[departingBusNumber + transportStartingTimes.Count * stopNumber]);

                schedule.InsertTime(stops[stopNumber].StopId, times);
            }

            return schedule;
        }

        private List<TimeSpan> CalculateStartingTimes(TrafiApiTimeJSON trafiApiTimeJSON) 
        {
            var departureTimes = new List<TimeSpan>();

            var departureTime = 0;
            for(int i = 0; i < trafiApiTimeJSON.TimeDiffsCounts.Count; i++)
            {
                for(int j = 0; j < trafiApiTimeJSON.TimeDiffsCounts[i]; j++)
                {
                    departureTime += trafiApiTimeJSON.TimeDiffsValues[i];
                    departureTimes.Add(new TimeSpan(0, departureTime, 0));
                }
            }

            return departureTimes;
        }

        private List<TimeSpan> CalculateArrivalTimes(TrafiApiDurationJSON trafiApiDurationJSON) 
        {
            var arrivalTimes = new List<TimeSpan>();

            var arrivalTime = 0;
            for(int i = 0; i < trafiApiDurationJSON.DurationValuesDiff.Count; i++)
            {
                arrivalTime += trafiApiDurationJSON.DurationValuesDiff[i];
                for(int j = 0; j < trafiApiDurationJSON.DurationCounts[i]; j++)
                    arrivalTimes.Add(new TimeSpan(0, arrivalTime, 0));
            }

            return arrivalTimes;
        }
    }
}