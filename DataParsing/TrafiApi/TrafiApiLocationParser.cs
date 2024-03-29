using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Krafi.DataObjects;

namespace Krafi.DataParsing.TrafiApi
{
    public class TrafiApiLocationParser : ILocationParser 
    {
        private string _locationsJSON;

        public TrafiApiLocationParser(string locationsJSON)
        {
            _locationsJSON = locationsJSON;
        }

        public List<ILocation> ParseLocations()
        {
            return ParseLocationsWithIds().Values.ToList();
        }

        public LocationIdMap<ILocation> ParseLocationsWithIds()
        {
            var stops = JsonConvert.DeserializeObject<List<TrafiApiStopJSON>>(_locationsJSON);
            var locations = new LocationIdMap<ILocation>();

            foreach(var stop in stops)
                locations.Add(stop.Id, new Location(stop.Id, stop.Name, stop.Coordinate.Lng, stop.Coordinate.Lat));

            return locations;
        }
    }
}