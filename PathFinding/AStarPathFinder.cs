using System;
using System.Collections.Generic;
using Krafi.DataObjects;
using Krafi.PathFinding.Graphs;
using Priority_Queue;

namespace Krafi.PathFinding
{
    public class AStarPathFinder : IPathFinder<AStarNode>
    {
        private IGraph<AStarNode> _graph;
        private IWeightCalculator _weightCalculator;
        private IWeightCalculator _heuristicCalculator;

        public AStarPathFinder(IGraph<AStarNode> graph, IWeightCalculator weightCalculator, IWeightCalculator heuristicCalculator) {
            _graph = graph;
            _weightCalculator = weightCalculator;
            _heuristicCalculator = heuristicCalculator;
        }

        public IPath FindPath(AStarNode startingNode, AStarNode endingNode, TimeSpan departureTime)
        {
            var visitedNodes = new HashSet<AStarNode>();
            var nodesToVisit = new SimplePriorityQueue<AStarNode>();
            var temporaryArrivalTimes = new Dictionary<INode, TimeSpan>();
            nodesToVisit.Enqueue(startingNode, 0);

            temporaryArrivalTimes[startingNode] = departureTime;
            startingNode.Weight = 0;
            startingNode.HeuristicWeight = 0;

            while(nodesToVisit.Count > 0) 
            {
                var currentNode = nodesToVisit.Dequeue();

                if(currentNode == endingNode)
                    break;

                foreach(var transit in currentNode.Transits)
                {
                    var endNode = (AStarNode)transit.EndNode;

                    var potentialWeight = _weightCalculator.CalculateWeight(transit, temporaryArrivalTimes[currentNode]) + currentNode.Weight;
                    var potentialHeuristic = _heuristicCalculator.CalculateWeight(transit, temporaryArrivalTimes[currentNode]);

                    var weightUpdated = false;
                    if (potentialWeight < endNode.Weight)
                    {
                        weightUpdated = true;
                        endNode.Weight = potentialWeight;
                        endNode.HeuristicWeight = potentialHeuristic;
                        endNode.FastestTransit = transit;

                        var intermediateDepartureTime = transit.Transport.Schedule.GetClosestDepartureTime(currentNode.Location.Id, temporaryArrivalTimes[currentNode]);
                        temporaryArrivalTimes[endNode] = intermediateDepartureTime + transit.Transport.TravelTime(transit.StartNode.Location, transit.EndNode.Location, intermediateDepartureTime);

                        transit.DepartureTime = intermediateDepartureTime;
                        transit.ArrivalTime = temporaryArrivalTimes[endNode];
                    }

                    if(!visitedNodes.Contains(endNode))
                    {
                        var isInVisitList = nodesToVisit.Contains(endNode);

                        if (!isInVisitList)
                            nodesToVisit.EnqueueWithoutDuplicates(endNode, (float)endNode.TotalWeight);
                        else if(isInVisitList && weightUpdated)
                            nodesToVisit.UpdatePriority(endNode, (float)endNode.TotalWeight);
                    }
                }

                visitedNodes.Add(currentNode);
            }

            return BacktrackPath(endingNode);
        }

        private IPath BacktrackPath(INode endNode)
        {
            var path = new Path();

            var currentNode = endNode;
            while(true)
            {
                if(currentNode.FastestTransit == null)
                    break;

                path.Add(currentNode.FastestTransit);
                currentNode = (AStarNode)currentNode.FastestTransit.StartNode;
            }

            return path;
        }
    }
}