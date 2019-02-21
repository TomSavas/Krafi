using System;
using System.Collections.Generic;
using Krafi.DataObjects;

namespace Krafi.PathFinding.Graphs
{
    public class Graph<T> : IGraph<T> where T : INode
    {
        public LocationIdMap<T> Nodes { get; }

        public Graph()
        {
            Nodes = new LocationIdMap<T>();
        }

        public void Reset() 
        {
            foreach(var node in Nodes.Values)
            {
                node.FastestTransit = null;

                foreach(var transit in node.Transits)
                {
                    transit.ArrivalTime = new TimeSpan();
                    transit.DepartureTime = new TimeSpan();
                }
            }
        }
    }
}