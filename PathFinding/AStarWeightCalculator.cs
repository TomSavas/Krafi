using System;
using Krafi.DataObjects;

namespace Krafi.PathFinding
{
    public class AStarWeightCalculator : IWeightCalculator 
    {
        public double CalculateWeight(ITransit transit, TimeSpan departureTime)
        {
            var startingLocation = transit.StartNode.Location;
            var endingLocation = transit.StartNode.Location;

            //Possibly have to adjust to work nicely with the heuristics
            return transit.Transport.TravelTime(startingLocation, endingLocation, departureTime).TotalMinutes; 
        }
    }
}