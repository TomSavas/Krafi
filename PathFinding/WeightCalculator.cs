using System;
using Krafi.DataObjects;

namespace Krafi.PathFinding
{
    public class WeightCalculator : IWeightCalculator 
    {
        public double CalculateWeight(ITransit transit, TimeSpan departureTime)
        {
            var startingLocation = transit.StartNode.Location;
            var endingLocation = transit.EndNode.Location;

            var actualDepartureTime = transit.Transport.GetClosestDepartureTime(transit.StartNode.Location, departureTime);
            var waitTime = actualDepartureTime - departureTime;
            var travelTime = transit.Transport.TravelTime(startingLocation, endingLocation, departureTime);

            return (waitTime + travelTime).TotalMinutes; 
        }
    }
}