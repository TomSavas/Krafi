using System;

namespace Krafi.DataObjects 
{
    public class Location : ILocation
    {
        private const double DEG_TO_METRES = 10000;

        public string Alias { get; }
        public double Longitude { get; }
        public double Latitude { get; }

        public Location(string alias, double longitude, double latitude) 
        {
            Alias = alias;
            Longitude = longitude;
            Latitude = latitude;
        }

        public double DistanceInMetres(ILocation otherLocation) 
        {
            return DEG_TO_METRES * Distance(otherLocation);
        }

        public double Distance(ILocation otherLocation) 
        {
            return Math.Sqrt(DistanceSquared(otherLocation));
        }

        public double DistanceSquared(ILocation otherLocation) 
        {
            // Inaccurate because of the curvature of the Earth, however, the distances we are concerned with are too
            // small to be affected by this. Worth the consideration, but efficiency implications must be investigated.
            return Math.Pow(Longitude - otherLocation.Longitude, 2) + Math.Pow(Latitude - otherLocation.Latitude, 2);
        }
    }
}