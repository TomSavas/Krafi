using System;
using Krafi.DataObjects;

namespace Krafi.PathFinding
{
    public class PenalisingWeightCalculator : WeightCalculatorWrapper 
    {
        private const double WALKING_PENALTY = 5;

        public PenalisingWeightCalculator(IWeightCalculator _baseWeightCalculator) : base(_baseWeightCalculator) {}

        public override double CalculateWeight(ITransit transit, TimeSpan departureTime)
        {
            var transportTypePenalty = transit.Transport.Alias == "Feet" ? WALKING_PENALTY : 0d;

            return base.CalculateWeight(transit, departureTime) + transportTypePenalty;
        }
    }
}