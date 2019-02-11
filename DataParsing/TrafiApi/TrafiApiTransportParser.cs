using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Krafi.DataObjects;
using Krafi.DataObjects.Vehicles;

namespace Krafi.DataParsing.TrafiApi
{
    public class TrafiApiTransportParser : ITransportParser 
    {
        private string _transportAndScheduleJSON;

        public TrafiApiTransportParser(string transportAndScheduleJSON)
        {
            _transportAndScheduleJSON = transportAndScheduleJSON;
        }

        public List<ITransport> ParseTransports(Dictionary<string, ILocation> stops) 
        {
            var trafiTransports = JsonConvert.DeserializeObject<TrafiApiScheduleJSON>(_transportAndScheduleJSON).Schedules;
            var transports = new List<ITransport>();

            foreach(var trafiTransport in trafiTransports)
            {
                foreach(var track in trafiTransport.Tracks)
                {
                    var destinations = new Dictionary<string, ILocation>();

                    foreach(var stop in track.Stops)
                        destinations.TryAdd(stops[stop.StopId].Alias, stops[stop.StopId]);

                    //For now pick the first timetable. 
                    var departureTimes = CalculateDepartureTimes(track.Timetables[0].Times);
                    var arrivalTimes = CalculateArrivalTimes(track.Timetables[0].Durations);

                    var schedule = new Schedule();
                    for(int stopNumber = 0; stopNumber < track.Stops.Count; stopNumber++)
                    {
                        var times = new List<TimeSpan>();
                        for(int departingBusNumber = 0; departingBusNumber < departureTimes.Count; departingBusNumber++)
                            times.Add(departureTimes[departingBusNumber] + arrivalTimes[departingBusNumber * track.Stops.Count + stopNumber]);

                        schedule.InsertTime(stops[track.Stops[stopNumber].StopId].Alias + track.Stops[stopNumber].StopId, times);
                    }

                    transports.Add(new PublicTransport(trafiTransport.Name, track.Name, destinations, schedule));
                }
            }

            return transports;
        }

        private List<TimeSpan> CalculateDepartureTimes(TrafiApiTimeJSON trafiApiTimeJSON) 
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