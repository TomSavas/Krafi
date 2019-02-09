using System;
using System.Collections;
using System.Collections.Generic;

namespace Krafi.DataObjects
{
    public interface IPath : IEnumerable<ITransit>
    {
        LinkedList<ITransit> Path { get; }
    }
}