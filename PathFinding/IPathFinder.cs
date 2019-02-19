using System;
using System.Collections.Generic;
using Krafi.DataObjects;

namespace Krafi.PathFinding
{
    public interface IPathFinder<T> where T : INode
    {
        IPath FindPath(T startingNode, T endingNode, TimeSpan departureTime);
    }
}