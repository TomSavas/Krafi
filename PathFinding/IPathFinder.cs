using System;
using System.Collections.Generic;
using Krafi.DataObjects;

namespace Krafi.PathFinding
{
    public interface IPathFinder 
    {
        IPath FindPath(INode startingNode, INode endingNode, DateTime departureTime);
    }
}