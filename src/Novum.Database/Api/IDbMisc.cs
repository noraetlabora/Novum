using System.Collections.Generic;

namespace Novum.Database.Api
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
        Dictionary<string, Novum.Data.CancellationResason> GetCancellationReason();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<string, Novum.Data.ServiceArea> GetServiceAreas();


    }
}