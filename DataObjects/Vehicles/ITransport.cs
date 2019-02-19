using System;
using System.Collections.Generic;
using Krafi.PathFinding;

namespace Krafi.DataObjects.Vehicles
{
    public interface ITransport 
    {
        string Alias { get; }
        ISchedule Schedule { get; }

        /// <summary>
        /// Calculates the time that will take to travel from the given location to the next
        /// closest location if departed at the next closest bus departure after the given time.
        /// </summary>
        /// <param name="startLocation"></param>
        /// <param name="endLocation"></param>
        /// <param name="departureTime"></param>
        /// <returns>Time that the transit between given locations will take.</returns>
        TimeSpan TravelTime(ILocation startLocation, ILocation endLocation, TimeSpan departureTime);

        bool IsDestinationReachable(ILocation location);
        bool IsDestinationReachable(string locationID);

        bool HasNextLocation(ILocation location);
        bool HasNextLocation(string locationID);

        ILocation GetNextLocation(ILocation location);
        ILocation GetNextLocation(string locationID);
    }
}