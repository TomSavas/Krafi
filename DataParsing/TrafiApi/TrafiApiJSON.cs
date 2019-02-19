using System.Collections.Generic;
using Newtonsoft.Json;

namespace Krafi.DataParsing.TrafiApi
{
    public class TrafiApiCoordinateJSON
    {
        [JsonProperty("Lat")]
        public double Lat { get; set; }
        [JsonProperty("Lng")]
        public double Lng { get; set; }
    }

    public class TrafiApiStopJSON
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Coordinate")]
        public TrafiApiCoordinateJSON Coordinate { get; set; } 
    }

    public class TrafiApiScheduleJSON 
    {
        [JsonProperty("Schedules")]
        public List<TrafiApiTransportJSON> Schedules { get; set; }
    }

    public class TrafiApiTransportJSON
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("LongName")]
        public string LongName { get; set; }
        [JsonProperty("Tracks")]
        public List<TrafiApiTrackJSON> Tracks { get; set; } 
    }

    public class TrafiApiTrackJSON
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("IsHidden")]
        public bool IsHidden { get; set; }
        [JsonProperty("Timetables")]
        public List<TrafiApiTimetableJSON> Timetables { get; set; }
        [JsonProperty("Stops")]
        public List<TrafiApiTimetableStopJSON> Stops { get; set; }
    }

    public class TrafiApiTimetableStopJSON
    {
        [JsonProperty("StopId")]
        public string StopId { get; set; }
    }

    public class TrafiApiTimetableJSON
    {
        [JsonProperty("Times")]
        public TrafiApiTimeJSON Times { get; set; }
        [JsonProperty("Durations")]
        public TrafiApiDurationJSON Durations { get; set; }
    }

    public class TrafiApiTimeJSON
    {
        [JsonProperty("TimeDiffsValues")]
        public List<int> TimeDiffsValues { get; set; }
        [JsonProperty("TimeDiffsCounts")]
        public List<int> TimeDiffsCounts { get; set; }
    }

    public class TrafiApiDurationJSON
    {
        [JsonProperty("DurationValuesDiff")]
        public List<int> DurationValuesDiff { get; set; }
        [JsonProperty("DurationCounts")]
        public List<int> DurationCounts { get; set; }
    }
}