using System.Collections.Generic;
using Krafi.DataObjects;
using Krafi.DataObjects.Vehicles;

namespace Krafi.PathFinding.Graphs
{
    public interface IGraphFormer 
    {
        IGraph FormGraph(List<ILocation> locations, List<ITransport> transports);
    }
}