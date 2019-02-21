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

        public List<ITransport> ParseTransports(LocationIdMap<ILocation> stops) 
        {
            var trafiTransports = JsonConvert.DeserializeObject<TrafiApiScheduleJSON>(_transportAndScheduleJSON).Schedules;
            var transports = new List<ITransport>();

            foreach(var trafiTransport in trafiTransports)
            {
                foreach(var track in trafiTransport.Tracks)
                {
                    var successiveDestinations = new List<ILocation>();
                    for(int i = 0; i < track.Stops.Count; i++)
                        successiveDestinations.Add(stops[track.Stops[i].StopId]);

                    //For now pick the first timetable. 
                    var scheduleParser = new TrafiApiScheduleParser();
                    var schedule = scheduleParser.Parse(track.Timetables[0].Times, track.Timetables[0].Durations, track.Stops);

                    transports.Add(new PublicTransport(trafiTransport.Name, track.Name, successiveDestinations, schedule));
                }
            }

            return transports;
        }
    }
}