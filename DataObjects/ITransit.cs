using System;
using Krafi.PathFinding;
using Krafi.DataObjects.Vehicles;

namespace Krafi.DataObjects
{
    public interface ITransit 
    {
        INode StartNode { get; set; }
        DateTime DepartureTime { get; set; }
        INode EndNode { get; set; }
        DateTime ArrivalTime { get; set; }
        ITransport Transport { get; set; }
    }
}