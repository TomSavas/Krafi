using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;
using Krafi.DataObjects;
using Krafi.DataObjects.Vehicles;
using Krafi.DataParsing;
using Krafi.DataParsing.TrafiApi;
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
            string stopJSON;
            string transportAndScheduleJSON;
            using (WebClient wc = new WebClient())
            {
                stopJSON = wc.DownloadString("https://api.trafi.com/api/stops?userLocationId=kaunas");
                transportAndScheduleJSON = wc.DownloadString("https://api.trafi.com/api/v3/schedules?userLocationId=kaunas");
            }

            ILocationParser locationParser = new TrafiApiLocationParser(stopJSON);
            var locationsWithIds = ((TrafiApiLocationParser)locationParser).ParseLocationsWithIds();

            ITransportParser transportParser = new TrafiApiTransportParser(transportAndScheduleJSON);
            var transports = transportParser.ParseTransports(locationsWithIds);

            IGraphFormer<AStarNode> graphFormer = new GraphFormer<AStarNode>();
            var stopGraph = graphFormer.FormGraph(locationsWithIds, transports);
    
            ITransitAdder pedestrianPathAdder = new PedestrianTransitAdder();
            stopGraph = pedestrianPathAdder.AddTransits(stopGraph);

            IWeightCalculator weightCalculator = new PenalisingWeightCalculator(new WeightCalculator());
            IWeightCalculator heuristicCalculator = new HeuristicCalculator();
            IPathFinder<AStarNode> pathFinder = new AStarPathFinder(stopGraph, weightCalculator, heuristicCalculator);

            IInputReader inputReader = new ConsoleInputReader();
            IOutputWriter outputWriter = new ConsoleOutputWriter();
            var stopWatch = new Stopwatch();

            var startingStopID = ((ConsoleInputReader)inputReader).ReadStopID(locationsWithIds);
            var endingStopID = ((ConsoleInputReader)inputReader).ReadStopID(locationsWithIds);
            var departureTime = inputReader.ReadTime();

            stopWatch.Reset();
            stopWatch.Start();
            var path = pathFinder.FindBestPath(stopGraph.Nodes[startingStopID], stopGraph.Nodes[endingStopID], departureTime);
            stopWatch.Stop();

            path.Squash();
            outputWriter.WriteElapsedTime(stopWatch.Elapsed);
            outputWriter.WritePath(path);
        }
    }
}
