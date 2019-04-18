using System.Collections.Generic;

namespace Nt.Database.Api
{
    /// <summary>
    /// Interface for miscellaneous Api
    /// </summary>
    public interface IDbMisc
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<string, Nt.Data.CancellationResason> GetCancellationReason();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<string, Nt.Data.ServiceArea> GetServiceAreas();

    }
}