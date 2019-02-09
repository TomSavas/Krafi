using System.Collections.Generic;

namespace Krafi.PathFinding.Graphs
{
    public interface IGraph 
    {
        Dictionary<string, INode> Nodes { get; }
    }
}