using System;
using System.Collections.Generic;
using System.Linq;
using Krafi.DataObjects;
using Krafi.DataObjects.Vehicles;

namespace Krafi.PathFinding.Graphs
{
    public class GraphFormer<T> : IGraphFormer<T> where T : INode, new()
    {
        public IGraph<T> FormGraph(LocationIdMap<ILocation> locations, List<ITransport> transports) 
        {
            var graph = new Graph<T>();

            //Don't want to use reflections, so use an empty constructor and add location later on
            foreach(var location in locations)
                graph.Nodes.Add(location.Key, new T());

            foreach(var location in locations)
            {
                var locationNode = graph.Nodes[location.Key];
                locationNode.Location = location.Value;
                var transits = new List<ITransit>();

                foreach(var transport in transports.Where(trans => trans.IsDestinationReachable(location.Value.Id) && trans.HasNextLocation(location.Value)))
                {
                    var transit = new Transit();
                    transit.StartNode = locationNode;
                    transit.EndNode = graph.Nodes[transport.GetNextLocation(transit.StartNode.Location.Id).Id];
                    transit.Transport = transport;

                    transits.Add(transit);
                }
                locationNode.Transits.AddRange(transits);
            }

            return graph;
        }
    }
}