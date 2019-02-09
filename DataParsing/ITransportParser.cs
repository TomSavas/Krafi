using System.Collections.Generic;
using Krafi.DataObjects.Vehicles;

namespace Krafi.DataParsing
{
    public interface ITransportParser 
    {
        List<ITransport> ParseTransports();
    }
}