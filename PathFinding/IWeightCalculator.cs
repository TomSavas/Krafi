using System;
using Krafi.DataObjects;

namespace Krafi.PathFinding
{
    public interface IWeightCalculator 
    {
        double CalculateWeight(ITransit transit, DateTime departureTime);
    }
}