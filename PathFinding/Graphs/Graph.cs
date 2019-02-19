using System;
using System.Collections.Generic;

namespace Krafi.PathFinding.Graphs
{
    public class Graph<T> : IGraph<T> where T : INode
    {
        public Dictionary<string, T> Nodes { get; }

        public Graph()
        {
            Nodes = new Dictionary<string, T>();
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