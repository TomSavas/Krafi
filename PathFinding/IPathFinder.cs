using System;
using System.Collections.Generic;
using Krafi.DataObjects;

namespace Krafi.PathFinding
{
    public interface IPathFinder<T> where T : INode
    {
        IPath FindFastestPath(T startingNode, T endingNode, TimeSpan departureTime);
        IPath FindBestPath(T startingNode, T endingNode, TimeSpan departureTime);
    }
}