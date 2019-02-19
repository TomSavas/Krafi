using System;
using System.Collections.Generic;
using Krafi.PathFinding;

using StopID = System.String;
using StopAlias = System.String;

namespace Krafi.DataObjects.Vehicles
{
    public class PublicTransport : ITransport
    {
        public string Alias { get; }
        public string TrackName { get; }
        public ISchedule Schedule { get; }

        private List<ILocation> _successiveDestinations { get; }
        private Dictionary<StopID, ILocation> _destinations { get; }

        public PublicTransport(string alias, string trackName, List<ILocation> successiveDestinations, ISchedule schedule) 
        {
            Alias = alias;
            TrackName = trackName;
            Schedule = schedule;

            _successiveDestinations = successiveDestinations;
            _destinations = new Dictionary<StopID, ILocation>();
            foreach(var destination in _successiveDestinations) 
                _destinations.TryAdd(destination.Id, destination);
            
        }

        public TimeSpan TravelTime(ILocation startLocation, ILocation endLocation, TimeSpan departureTime) 
        {
            var actualDepartureTime = Schedule.GetClosestDepartureTime(startLocation.Id, departureTime);
            var actualArrivalTime = Schedule.GetClosestDepartureTime(endLocation.Id, actualDepartureTime);

            return actualArrivalTime.Subtract(actualDepartureTime);
        }

        public bool IsDestinationReachable(ILocation location)
        {
            return IsDestinationReachable(location.Id);
        }

        public bool IsDestinationReachable(string locationId)
        {
            return _destinations.ContainsKey(locationId);
        }

        public bool HasNextLocation(ILocation location) 
        {
            return HasNextLocation(location.Id);
        }

        public bool HasNextLocation(string locationID) 
        {
            for(int i = 0; i < _successiveDestinations.Count - 1; i++)
                if(locationID == _successiveDestinations[i].Id)
                    return true;

            return false;
        }

        public ILocation GetNextLocation(ILocation location)
        {
            return GetNextLocation(location.Id);
        }

        public ILocation GetNextLocation(string locationID)
        {
            var returnNext = false;

            foreach(var destination in _successiveDestinations)
            {
                if(returnNext)
                    return destination;

                if(destination.Id == locationID)
                    returnNext = true;
            }

            throw new Exception("No next location found.");
        }
    }
}