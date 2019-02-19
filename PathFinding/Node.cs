using System;
using System.Collections.Generic;
using Krafi.DataObjects;

namespace Krafi.PathFinding
{
    public class Node : INode 
    {
        public ILocation Location { get; set; }
        public List<ITransit> Transits { get; set; }
        public ITransit FastestTransit { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public TimeSpan DepartureTime { get; set; }

        public Node() : this(new Location()) {}

        public Node(ILocation location) : this(location, new List<ITransit>()) {}

        public Node(ILocation location, List<ITransit> transits)
        {
            Location = location;
            Transits = transits;
            FastestTransit = null;
            ArrivalTime = new TimeSpan();
            DepartureTime = new TimeSpan();
        }
    }
}