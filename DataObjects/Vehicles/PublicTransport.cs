using System;
using System.Collections.Generic;
using Krafi.PathFinding;

using StopAlias = System.String;

namespace Krafi.DataObjects.Vehicles
{
    public class PublicTransport : ITransport
    {
        public string Alias { get; }
        public string TrackName { get; }
        public Dictionary<StopAlias, ILocation> Destinations { get; }
        public ISchedule Schedule { get; }

        public PublicTransport(string alias, string trackName, Dictionary<string, ILocation> destinations, ISchedule schedule) 
        {
            Alias = alias;
            TrackName = trackName;
            Destinations = destinations;
            Schedule = schedule;
        }

        public TimeSpan TravelTime(ILocation startLocation, ILocation endLocation, TimeSpan departureTime) {
            var actualDepartureTime = Schedule.GetClosestTime(startLocation.Alias, departureTime);
            var actualArrivalTime = Schedule.GetClosestTime(endLocation.Alias, actualDepartureTime);

            return actualArrivalTime.Subtract(departureTime);
        }
    }
}