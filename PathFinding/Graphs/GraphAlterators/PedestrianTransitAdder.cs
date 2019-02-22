using System;
using System.Collections.Generic;
using Krafi.DataObjects;
using Krafi.DataObjects.Vehicles;

namespace Krafi.PathFinding.Graphs.GraphAlterators
{
    public class PedestrianTransitAdder : ITransitAdder
    {
        public IGraph<T> AddTransits<T>(IGraph<T> graph) where T : INode
        {
            var feetTransportation = new Feet();

            foreach(var node in graph.Nodes)
            {
                foreach(var arrivalNode in graph.Nodes)
                {
                    if(node.Key != arrivalNode.Key)
                        node.Value.Transits.Add(BuildPedestrianTransit(node.Value, arrivalNode.Value, feetTransportation));
                }
            }

            return graph;
        }

        private ITransit BuildPedestrianTransit<T>(T startNode, T endNode, ITransport transport) where T : INode
        {
            var pedestrianTransit = new Transit();

            pedestrianTransit.StartNode = startNode;
            pedestrianTransit.EndNode = endNode;
            pedestrianTransit.Transport = transport;

            return pedestrianTransit;
        }
    }
}