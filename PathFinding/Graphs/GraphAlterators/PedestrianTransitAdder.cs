using System;
using System.Collections.Generic;
using Krafi.DataObjects;
using Krafi.DataObjects.Vehicles;

namespace Krafi.PathFinding.Graphs.GraphAlterators
{
    public class PedestrianTransitAdder : ITransitAdder
    {
        public IGraph AddTransits(IGraph graph) 
        {
            foreach(var node in graph.Nodes)
            {
                foreach(var arrivalNode in graph.Nodes)
                {
                    if(node.Key != arrivalNode.Key)
                        node.Value.Transits.Add(BuildPedestrianTransit(node.Value, arrivalNode.Value));
                }
            }

            return graph;
        }

        private ITransit BuildPedestrianTransit(INode startNode, INode endNode)
        {
            var pedestrianTransit = new Transit();

            pedestrianTransit.StartNode = startNode;
            pedestrianTransit.EndNode = endNode;
            pedestrianTransit.Transport = new Feet();

            return pedestrianTransit;
        }
    }
}