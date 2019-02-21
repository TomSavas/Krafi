using System.Collections.Generic;
using Krafi.DataObjects;
using Krafi.DataObjects.Vehicles;

namespace Krafi.PathFinding.Graphs
{
    public interface IGraphFormer<T> where T : INode
    {
        IGraph<T> FormGraph(LocationIdMap<ILocation> locations, List<ITransport> transports);
    }
}