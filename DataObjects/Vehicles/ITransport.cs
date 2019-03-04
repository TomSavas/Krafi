using System;
using System.Collections.Generic;
using Krafi.PathFinding;

namespace Krafi.DataObjects.Vehicles
{
    public interface ITransport 
    {
        string Alias { get; }

        TimeSpan TravelTime(ILocation startLocation, ILocation endLocation, TimeSpan departureTime);
        TimeSpan GetClosestDepartureTime(ILocation location, TimeSpan time);

        bool IsDestinationReachable(ILocation location);
        bool IsDestinationReachable(string locationId);

        bool IsTransitPossible(ILocation startLocation, ILocation endLocation);
        bool IsTransitPossible(string startLocationId, string endLocationId);

        bool HasNextLocation(ILocation location);
        bool HasNextLocation(string locationId);

        ILocation GetNextLocation(ILocation location);
        ILocation GetNextLocation(string locationId);
    }
}