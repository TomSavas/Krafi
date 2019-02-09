using System;

namespace Krafi.DataObjects 
{
    public interface ILocation 
    {
        string Alias { get; }
        double Longitude { get; }
        double Latitude { get; }

        double DistanceInMetres(ILocation otherLocation);
        double Distance(ILocation otherLocation);
        double DistanceSquared(ILocation otherLocation);
    }
}