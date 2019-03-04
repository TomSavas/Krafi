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
    }
}