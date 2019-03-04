using System;
using System.Collections.Generic;
using Krafi.PathFinding;


namespace Krafi.DataObjects.Vehicles
{
    public class PublicTransport : ITransport
    {
        public string Alias { get; }
        public string TrackName { get; }
        public ISchedule Schedule { get; }

        private List<ILocation> _successiveDestinations { get; }
        private LocationIdMap<ILocation> _destinations { get; }
        private LocationIdMap<int> _destinationIndex { get; }

        public PublicTransport(string alias, string trackName, List<ILocation> successiveDestinations, ISchedule schedule) 
        {
            Alias = alias;
            TrackName = trackName;
            Schedule = schedule;

            _successiveDestinations = successiveDestinations;
            _destinations = new LocationIdMap<ILocation>();
            _destinationIndex = new LocationIdMap<int>();

            for(int i = 0; i < _successiveDestinations.Count; i++)
            {
                _destinations.TryAdd(_successiveDestinations[i].Id, _successiveDestinations[i]);
                _destinationIndex.TryAdd(_successiveDestinations[i].Id, i);

            }
        }

        public TimeSpan TravelTime(ILocation startLocation, ILocation endLocation, TimeSpan departureTime) 
        {
            TimeSpan actualDepartureTime;
            TimeSpan actualArrivalTime;

            if(AreSuccessive(startLocation, endLocation))
            {
                actualDepartureTime = Schedule.GetClosestDepartureTime(startLocation.Id, departureTime);
                actualArrivalTime = Schedule.GetClosestDepartureTime(endLocation.Id, actualDepartureTime);
            }
            else
            {
                actualDepartureTime = Schedule.GetClosestDepartureTime(startLocation.Id, departureTime);
                actualArrivalTime = actualDepartureTime + Schedule.GetTravelTime(startLocation.Id, endLocation.Id, actualDepartureTime);
            }

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

        public bool IsTransitPossible(ILocation startLocation, ILocation endLocation)
        {
            return IsTransitPossible(startLocation.Id, endLocation.Id);
        }

        public bool IsTransitPossible(string startLocationId, string endLocationId)
        {
            return _destinations.ContainsKey(startLocationId) && _destinations.ContainsKey(endLocationId) &&
                   _destinationIndex[startLocationId] <= _destinationIndex[endLocationId];
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

        private bool AreSuccessive(ILocation startLocation, ILocation endLocation)
        {
            return _destinationIndex[startLocation.Id] + 1 == _destinationIndex[endLocation.Id];
        }
    }
}