using System.Collections.Generic;

namespace Krafi.PathFinding.Graphs
{
    public interface IGraph<T> where T : INode
    {
        Dictionary<string, T> Nodes { get; }

        void Reset();
    }
}