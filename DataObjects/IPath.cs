using System;
using System.Collections;
using System.Collections.Generic;

namespace Krafi.DataObjects
{
    public interface IPath : IEnumerable<ITransit> 
    {
        // Squash consecutive transits with the same transportations to one
        void Squash();
    }
}