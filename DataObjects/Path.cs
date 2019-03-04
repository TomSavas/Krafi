using System;
using System.Collections;
using System.Collections.Generic;

namespace Krafi.DataObjects
{
    public class Path : IPath
    {
        private List<ITransit> _path { get; set; }

        public Path()
        {
            _path = new List<ITransit>();
        }

        public virtual void Add(ITransit transit)
        {
            _path.Add(transit);
        }

        public IEnumerator<ITransit> GetEnumerator()
        {
            for(int i = _path.Count - 1; i >= 0; i--)
                yield return _path[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for(int i = _path.Count - 1; i >= 0; i--)
                yield return _path[i];
        }
    }
}