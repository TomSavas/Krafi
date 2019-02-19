using System;

namespace Krafi.DataObjects 
{
    public class Location : ILocation
    {
        private const double _equatorialEarthRadius = 6378.1370D;
        private const double _degreesToRadians = (Math.PI / 180D);

        public string Id { get; }
        public string Alias { get; }
        public double Longitude { get; }
        public double Latitude { get; }

        public Location() : this("", "", 0d, 0d) {}

        public Location(string id, string alias, double longitude, double latitude) 
        {
            Id = id;
            Alias = alias;
            Longitude = longitude;
            Latitude = latitude;
        }

        public double DistanceInMetres(ILocation otherLocation) 
        {
            return HaversineInM(Latitude, Longitude, otherLocation.Latitude, otherLocation.Longitude);
        }

        private int HaversineInM(double lat1, double long1, double lat2, double long2)
        {
            return (int)(1000d * HaversineInKM(lat1, long1, lat2, long2));
        }

        // Thanks to this chap: https://stackoverflow.com/a/7595937
        private double HaversineInKM(double lat1, double long1, double lat2, double long2)
        {
            double dlong = (long2 - long1) * _degreesToRadians;
            double dlat = (lat2 - lat1) * _degreesToRadians;
            double a = Math.Pow(Math.Sin(dlat / 2d), 2d) + Math.Cos(lat1 * _degreesToRadians) * Math.Cos(lat2 * _degreesToRadians) * Math.Pow(Math.Sin(dlong / 2d), 2d);
            double c = 2D * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1D - a));
            double d = _equatorialEarthRadius * c;

            return d;
        }

    }
}