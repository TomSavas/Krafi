using System;
using System.Collections.Generic;
using Krafi.DataObjects;

namespace Krafi.PathFinding
{
    public interface INode 
    {
        ILocation Location { get; }
        List<ITransit> Transits { get; }
    }
}