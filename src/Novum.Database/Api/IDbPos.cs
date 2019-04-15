using System.Collections.Generic;

namespace Novum.Database.Api
{
    /// <summary>
    /// Interface for Printer Api
    /// </summary>
    public interface IDbPos
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        string GetPosId(string deviceId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="posId"></param>
        /// <returns></returns>
        string GetServiceAreaId(string posId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceAreaId"></param>
        /// <returns></returns>
        string GetServiceAreaName(string serviceAreaId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sercieAreaId"></param>
        /// <returns></returns>
        string GetPriceLevel(string sercieAreaId);
    }
}