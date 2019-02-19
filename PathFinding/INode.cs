using System;
using System.Collections.Generic;
using Krafi.DataObjects;

namespace Krafi.PathFinding
{
    public interface INode 
    {
        ILocation Location { get; set; }
        List<ITransit> Transits { get; set; }
        ITransit FastestTransit { get; set; }
    }
}