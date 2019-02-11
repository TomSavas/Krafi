using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Krafi.DataObjects
{
    public class Schedule : ISchedule
    {
        //Temporary implementation for testing
        List<KeyValuePair<string, List<TimeSpan>>> _times = new List<KeyValuePair<string, List<TimeSpan>>>();

        public TimeSpan GetClosestTime(string stopName, TimeSpan time) 
        {
            throw new NotImplementedException();
        }

        public IEnumerator<TimeSpan> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void InsertTime(string stopName, List<TimeSpan> times) 
        {
            _times.Add(new KeyValuePair<string, List<TimeSpan>>(stopName, times));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}