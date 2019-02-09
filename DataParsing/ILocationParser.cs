using System.Collections.Generic;
using Krafi.DataObjects;

namespace Krafi.DataParsing
{
    public interface ILocationParser 
    {
        List<ILocation> ParseLocations();
    }
}