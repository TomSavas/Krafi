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

        public void Squash()
        {
            var squashedPath = new List<ITransit>();
            squashedPath.Add(_path[0]);

            for(int i = 1; i < _path.Count; i++)
            {
                if(_path[i].Transport.Alias == squashedPath[squashedPath.Count - 1].Transport.Alias)
                {
                    squashedPath[squashedPath.Count - 1].DepartureTime = _path[i].DepartureTime;
                    squashedPath[squashedPath.Count - 1].StartNode = _path[i].StartNode;
                }
                else
                {
                    squashedPath.Add(_path[i]);
                }
            }

            _path = squashedPath;
        }

        public virtual void Add(ITransit transit)
        {
            _path.Add(transit);
        }

        public IEnumerator<ITransit> GetEnumerator()
        {
            for(int i = _path.Count - 1; i >= 0; i--)
            {
                yield return _path[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for(int i = _path.Count - 1; i >= 0; i--)
            {
                yield return _path[i];
            }
        }
    }
}