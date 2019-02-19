using System;
using System.Collections.Generic;
using Krafi.DataObjects;

namespace Krafi.PathFinding
{
    public class NodeWrapper : INode 
    {
        public ILocation Location 
        {
            get => _underlyingNode.Location;
            set => _underlyingNode.Location = value;
        }
        public List<ITransit> Transits
        {
            get => _underlyingNode.Transits;
            set => _underlyingNode.Transits = value;
        }
        public ITransit FastestTransit
        {
            get => _underlyingNode.FastestTransit;
            set => _underlyingNode.FastestTransit = value;
        }

        private INode _underlyingNode;

        public NodeWrapper(INode baseNode)
        {
            _underlyingNode = baseNode;
        }
    }
}