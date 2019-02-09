using System;
using Krafi.PathFinding;
using Krafi.DataObjects.Vehicles;

namespace Krafi.DataObjects
{
    public interface ITransit 
    {
        INode StartNode { get; }
        DateTime DepartureTime { get; set; }
        INode EndNode { get; }
        DateTime ArrivalTime { get; set; }
        ITransport Transport { get; }
    }
}