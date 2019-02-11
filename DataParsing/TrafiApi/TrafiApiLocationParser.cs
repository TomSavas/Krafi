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
            return ParseLocationsWithIDs().Values.ToList();
        }

        public Dictionary<string, ILocation> ParseLocationsWithIDs()
        {
            var stops = JsonConvert.DeserializeObject<List<TrafiApiStopJSON>>(_locationsJSON);
            var locations = new Dictionary<string, ILocation>();

            foreach(var stop in stops)
                locations.Add(stop.Id, new Location(stop.Name, stop.Coordinate.Lng, stop.Coordinate.Lat));

            return locations;
        }
    }
}