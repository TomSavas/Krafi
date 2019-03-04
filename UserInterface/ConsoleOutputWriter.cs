using System;
using Krafi.DataObjects;
using Krafi.PathFinding;

namespace Krafi.UserInterface
{
    public class ConsoleOutputWriter : IOutputWriter 
    {
        public void WritePath(IPath path) 
        {
            foreach (var transit in path) 
            {
                System.Console.WriteLine(transit.StartNode.Location.Alias + " (" + transit.DepartureTime + ")" +
                                         " via " + transit.Transport.Alias + " -> " + 
                                         transit.EndNode.Location.Alias + " (" + transit.ArrivalTime + ")");
            }
        }

        public void WriteElapsedTime(TimeSpan elapsedTime) 
        {
            System.Console.WriteLine("Elapsed time: " + elapsedTime.TotalMilliseconds + "ms");
        }
    }
}