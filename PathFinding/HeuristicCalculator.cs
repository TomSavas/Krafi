using System;
using Krafi.DataObjects;
using Krafi.DataObjects.Vehicles;

namespace Krafi.PathFinding 
{
    public class HeuristicCalculator : IWeightCalculator
    {
        public double CalculateWeight(ITransit transit, TimeSpan departureTime) 
        {
            // Make sure, that the heuristic weight is always lower than any possible path from startNode to endNode.
            // This isn't a bulletproof way to ensure that, however, having an entity moving 10 times the speed of
            // a person seems reasonable enough, to say that it's going to be faster than any possible path.
            return new Feet().TravelTime(transit.StartNode.Location, transit.EndNode.Location, departureTime).TotalMinutes / 10;
        }
    }
}