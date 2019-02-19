using System;
using Krafi.DataObjects;

namespace Krafi.PathFinding
{
    public class PenalisingWeightCalculator : WeightCalculatorWrapper 
    {
        private const double WALKING_PENALTY = 3;
        private const double TRANSFER_PENALTY = 0;//20;

        public PenalisingWeightCalculator(IWeightCalculator _baseWeightCalculator) : base(_baseWeightCalculator) {}

        public override double CalculateWeight(ITransit transit, TimeSpan departureTime)
        {
            var transportTypePenalty = transit.Transport.Alias == "Feet" ? WALKING_PENALTY : 0d;
            var transferPenalty = transit.StartNode.FastestTransit == null || transit.StartNode.FastestTransit.Transport.Alias == transit.Transport.Alias ? 0 : TRANSFER_PENALTY;

            return base.CalculateWeight(transit, departureTime) + transportTypePenalty + transferPenalty;
        }
    }
}