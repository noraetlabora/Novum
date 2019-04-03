using System.Collections.Generic;

namespace Novum.Database.Api
{
    /// <summary>
    /// Interface for miscellaneous Api
    /// </summary>
    public interface IDbMisc
    {
        Dictionary<string, Novum.Data.CancellationResason> GetCancellationReason(string department);
        Dictionary<string, Novum.Data.ServiceArea> GetServiceAreas(string department);


    }
}