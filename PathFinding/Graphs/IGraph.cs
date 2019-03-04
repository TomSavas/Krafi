using System.Collections.Generic;
using Krafi.DataObjects;

namespace Krafi.PathFinding.Graphs
{
    public interface IGraph<T> where T : INode
    {
        LocationIdMap<T> Nodes { get; }
    }
}