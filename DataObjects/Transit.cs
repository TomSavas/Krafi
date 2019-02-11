using System;
using Krafi.PathFinding;
using Krafi.DataObjects.Vehicles;

namespace Krafi.DataObjects
{
    public class Transit : ITransit 
    {
        public INode StartNode { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public INode EndNode { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public ITransport Transport { get; set; }
    }
}