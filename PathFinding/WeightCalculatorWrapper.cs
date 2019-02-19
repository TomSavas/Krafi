using System;
using Krafi.DataObjects;

namespace Krafi.PathFinding
{
    public class WeightCalculatorWrapper : IWeightCalculator 
    {
        private IWeightCalculator _underlyingWeightCalculator;

        public WeightCalculatorWrapper(IWeightCalculator baseWeightCalculator)
        {
            _underlyingWeightCalculator = baseWeightCalculator;
        }

        public virtual double CalculateWeight(ITransit transit, TimeSpan departureTime) => _underlyingWeightCalculator.CalculateWeight(transit, departureTime);
    }
}