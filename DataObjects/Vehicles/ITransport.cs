using System;
using System.Collections.Generic;
using Krafi.PathFinding;

namespace Krafi.DataObjects.Vehicles
{
    public interface ITransport 
    {
        string Alias { get; }
        Dictionary<string, ILocation> Destinations { get; }
        ISchedule Schedule { get; }

        /// <summary>
        /// Calculates the time that will take to travel from the given location to the next
        /// closest location if departed at the given time.
        /// </summary>
        /// <param name="startLocation"></param>
        /// <param name="endLocation"></param>
        /// <param name="departureTime"></param>
        /// <returns>Time that the transit between locations will take.</returns>
        TimeSpan TravelTime(ILocation startLocation, ILocation endLocation, TimeSpan departureTime);
    }
}