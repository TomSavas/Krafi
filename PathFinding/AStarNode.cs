using System;
using System.Collections.Generic;
using Krafi.DataObjects;

namespace Krafi.PathFinding
{
    public class AStarNode : NodeWrapper 
    {
        public double HeuristicWeight { get; set; } = Double.MaxValue;
        public double Weight { get; set; } = Double.MaxValue;
        public double TotalWeight => HeuristicWeight + Weight;

        public AStarNode() : base(new Node()) {}

        public AStarNode(INode baseNode) : base(baseNode) {}
    }
}