using System;
using System.Collections.Generic;
using Krafi.PathFinding;

namespace Krafi.DataObjects.Vehicles
{
    public class Feet : ITransport 
    {
        private const double WALKING_SPEED = 1.4; // m/s

        public string Alias { get; }

        private ISchedule _schedule { get; }

        public Feet() 
        {
            Alias = "Feet";
            _schedule = new FeetSchedule();
        }

        public TimeSpan TravelTime(ILocation startLocation, ILocation endLocation, TimeSpan departureTime) {
            var travelDistance = startLocation.DistanceInMetres(endLocation);

            var travelTimeInSeconds = travelDistance / WALKING_SPEED;
            var travelTime = new TimeSpan(0, 0, (int)travelTimeInSeconds);

            return travelTime;
        }

        public TimeSpan GetClosestDepartureTime(ILocation location, TimeSpan time) 
        {
            return _schedule.GetClosestDepartureTime(location.Id, time);
        }

        public bool IsDestinationReachable(ILocation location) => true;

        public bool IsDestinationReachable(string locationId) => true;

        public bool IsTransitPossible(ILocation startLocation, ILocation endLocation) => true;

        public bool IsTransitPossible(string startLocationId, string endLocationId) => true;

        public bool HasNextLocation(ILocation location) => true;

        public bool HasNextLocation(string locationId) => true;

        public ILocation GetNextLocation(ILocation location)
        {
            return GetNextLocation(location.Id);
        }

        public ILocation GetNextLocation(string locationID) 
        {
            throw new Exception("Shouldn't try to get a next location from a feet transportation.");
        }
    }
}