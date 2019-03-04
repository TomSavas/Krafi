using System;
using System.Collections;
using System.Collections.Generic;
using Krafi.PathFinding;

namespace Krafi.DataObjects
{
    public class AStarPath : Path
    {
        public double Value { get; private set; }

        public AStarPath() : base()
        {
            Value = 0;
        }

        public override void Add(ITransit transit)
        {
            Value += ((AStarNode)transit.EndNode).TotalWeight;
            base.Add(transit);
        }
    }
}