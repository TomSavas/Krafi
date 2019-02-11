using System;
using System.Collections.Generic;
using Krafi.PathFinding;

namespace Krafi.DataObjects.Vehicles
{
    public class Feet : ITransport 
    {
        private const double WALKING_SPEED = 1.4; // m/s

        public string Alias { get; }
        public Dictionary<string, ILocation> Destinations { get; }
        public ISchedule Schedule { get; }

        public Feet() 
        {
            Alias = "Feet";
            Destinations = new Dictionary<string, ILocation>();
            Schedule = new Schedule();
        }

        public TimeSpan TravelTime(ILocation startLocation, ILocation endLocation, TimeSpan departureTim) {
            var travelDistance = startLocation.Distance(endLocation);

            var travelTimeInSeconds = travelDistance / WALKING_SPEED;
            var travelTime = new TimeSpan(0, 0, (int)travelTimeInSeconds);

            return travelTime;
        }
    }
}