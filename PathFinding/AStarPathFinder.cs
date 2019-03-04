using System;
using System.Collections.Generic;
using Krafi.DataObjects;
using Krafi.DataObjects.Vehicles;
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

        public IPath FindFastestPath(AStarNode startingNode, AStarNode endingNode, TimeSpan departureTime)
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

                        var intermediateDepartureTime = transit.Transport.GetClosestDepartureTime(currentNode.Location, temporaryArrivalTimes[currentNode]);
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

        public IPath FindBestPath(AStarNode startingNode, AStarNode endingNode, TimeSpan departureTime)
        {
            var paths = new SimplePriorityQueue<IPath>();

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

                    var pathWithStatus = FindDirectPath(transit, endingNode, temporaryArrivalTimes[transit.StartNode]);
                    if(pathWithStatus.Item1)
                        paths.Enqueue(pathWithStatus.Item2, (float)pathWithStatus.Item2.Value);
                    
                    var potentialWeight = _weightCalculator.CalculateWeight(transit, temporaryArrivalTimes[currentNode]) + currentNode.Weight;
                    var potentialHeuristic = _heuristicCalculator.CalculateWeight(transit, temporaryArrivalTimes[currentNode]);

                    var weightUpdated = false;
                    if (potentialWeight < endNode.Weight)
                    {
                        weightUpdated = true;
                        endNode.Weight = potentialWeight;
                        endNode.HeuristicWeight = potentialHeuristic;
                        endNode.FastestTransit = transit;

                        var intermediateDepartureTime = transit.Transport.GetClosestDepartureTime(currentNode.Location, temporaryArrivalTimes[currentNode]);
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

            var path = BacktrackPath(endingNode);
            paths.Enqueue(path, (float)path.Value);

            return paths.Dequeue();
        }

        // Returns bool representing if the path has been found the path itself
        private Tuple<bool, AStarPath> FindDirectPath(ITransit transit, AStarNode endingNode, TimeSpan time)
        {
            ITransit directTransit = null;

            var originalWeight = endingNode.Weight;
            var originalHeuristicWeight = endingNode.HeuristicWeight;

            // We are not interested in direct paths by feet, as it will most likely won't be the best path.
            // If it is A* will find it in the main part of the algorithm.
            if(transit.Transport.Alias != "Feet" && transit.Transport.IsTransitPossible(transit.StartNode.Location, endingNode.Location))
            {
                directTransit = new Transit();
                directTransit.Transport = transit.Transport;
                directTransit.StartNode = transit.StartNode;
                directTransit.EndNode = endingNode;

                var intermediateDepartureTime = directTransit.Transport.GetClosestDepartureTime(transit.StartNode.Location, time);
                var travelTime = directTransit.Transport.TravelTime(transit.StartNode.Location, endingNode.Location, intermediateDepartureTime);
                var arrivalTime = intermediateDepartureTime + travelTime;

                var potentialWeight = intermediateDepartureTime.Subtract(time).TotalMinutes + travelTime.TotalMinutes + ((AStarNode)transit.StartNode).Weight * 0.7;
                var potentialHeuristic = _heuristicCalculator.CalculateWeight(transit, time);
                endingNode.Weight = potentialWeight;
                endingNode.HeuristicWeight = potentialHeuristic;

                directTransit.DepartureTime = intermediateDepartureTime;
                directTransit.ArrivalTime = arrivalTime;
            }

            AStarPath path = BacktrackPath(directTransit);

            // Reset ending node weight as to not interfere with the main part of the algorithm
            endingNode.Weight = originalWeight;
            endingNode.HeuristicWeight = originalHeuristicWeight;

            return Tuple.Create(path.Value != 0, path);
        }

        private AStarPath BacktrackPath(ITransit transit)
        {
            var path = new AStarPath();

            if(transit == null)
                return path;

            path.Add(transit);
            return BacktrackPath(transit.StartNode, path);
        }

        private AStarPath BacktrackPath(INode endNode, AStarPath existingPath = null)
        {
            var path = existingPath ?? new AStarPath();

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