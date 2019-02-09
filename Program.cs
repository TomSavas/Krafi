using System;
using System.Collections.Generic;
using System.Diagnostics;
using Krafi.DataObjects;
using Krafi.DataParsing;
using Krafi.PathFinding;
using Krafi.PathFinding.Graphs;
using Krafi.PathFinding.Graphs.GraphAlterators;
using Krafi.UserInterface;

namespace Krafi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ILocationParser locationParser = new TrafiApiLocationParser();
            var locations = locationParser.ParseLocations("stops");

            IScheduleParser scheduleParser = new TrafiApiScheduleParser();
            ITransportParser transportParser = new TrafiApiTransportParser(scheduleParser);
            var transports = transportParser.ParseTransports(locations);

            IGraphFormer graphFormer = new GraphFormer();
            var stopGraph = graphFormer.FormGraph(locations, transports);

            ITransitAdder pedestrianPathAdder = new PedestrianTransitAdder();
            stopGraph = pedestrianPathAdder.AddTransits(stopGraph);

            IWeightCalculator weightCalculator = new AStarWeightCalculator();
            IWeightCalculator heuristicCalculator = new HeuristicCalculator();
            IPathFinder pathFinder = new AStarPathFinder(stopGraph, weightCalculator, heuristicCalculator);

            IInputReader inputReader = new ConsoleInputReader();
            IOutputWriter outputWriter = new ConsoleOutputWriter();
            var stopWatch = new Stopwatch();
            bool doMoreSearches = true;

            while (doMoreSearches)
            {
                var startingStop = inputReader.ReadStop();
                var endingStop = inputReader.ReadStop();
                var departureTime = inputReader.ReadTime();

                stopWatch.Reset();
                stopWatch.Start();
                var path = pathFinder.FindPath(stopGraph.Nodes[startingStop], stopGraph.Nodes[endingStop], departureTime);
                stopWatch.Stop();

                outputWriter.WriteElapsedTime(stopWatch.Elapsed);
                outputWriter.WritePath(path);

                doMoreSearches = inputReader.ReadDoMoreSearches();
            }
        }
    }
}
