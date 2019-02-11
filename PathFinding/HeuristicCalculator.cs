using System;
using Krafi.DataObjects;

namespace Krafi.PathFinding 
{
    public class HeuristicCalculator : IWeightCalculator
    {
        public double CalculateWeight(ITransit transit, TimeSpan departureTime) 
        {
            return transit.StartNode.Location.Distance(transit.EndNode.Location);
        }
    }
}