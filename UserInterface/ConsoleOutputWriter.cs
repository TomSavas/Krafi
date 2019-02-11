using System;
using Krafi.DataObjects;

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
                                         transit.StartNode.Location.Alias + " (" + transit.ArrivalTime + ")");
            }
        }

        public void WriteElapsedTime(TimeSpan elapsedTime) 
        {
            System.Console.WriteLine("Elapsed time: " + elapsedTime.TotalMilliseconds + "ms");
        }
    }
}