using System;

namespace Krafi.DataObjects 
{
    public interface ILocation 
    {
        string Id { get; }
        string Alias { get; }
        double Longitude { get; }
        double Latitude { get; }

        double DistanceInMetres(ILocation otherLocation);
    }
}