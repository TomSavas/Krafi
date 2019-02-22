using System.Collections;
using System.Collections.Generic;

namespace Krafi.DataObjects
{
    public class LocationIdMap<T> : ILocationIdMap<T>
    {
        private Dictionary<string, T> _dict;

        public T this[string key] { get => _dict[key]; set => _dict[key] = value; }

        public ICollection<string> Keys => _dict.Keys;

        public ICollection<T> Values => _dict.Values;

        public int Count => _dict.Count;

        public bool IsReadOnly => false;

        public LocationIdMap()
        {
            _dict = new Dictionary<string, T>();
        }

        public void Add(string key, T value)
        {
            _dict.Add(key, value);
        }

        public void Add(KeyValuePair<string, T> item)
        {
            _dict.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _dict.Clear();
        }

        public bool Contains(KeyValuePair<string, T> item)
        {
            return _dict.ContainsKey(item.Key) && _dict.ContainsValue(item.Value);
        }

        public bool ContainsKey(string key)
        {
            return _dict.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, T>[] array, int arrayIndex)
        {
            foreach(var item in _dict)
            {
                array[arrayIndex] = item;
                arrayIndex++;
            }
        }

        public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
        {
            return _dict.GetEnumerator();
        }

        public bool Remove(string key)
        {
            return _dict.Remove(key);
        }

        public bool Remove(KeyValuePair<string, T> item)
        {
            return _dict.Remove(item.Key);
        }

        public bool TryGetValue(string key, out T value)
        {
            return _dict.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dict.GetEnumerator();
        }
    }
}