using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nt.Database.Api
{
    /// <summary>
    /// Interface for Printer Api
    /// </summary>
    public interface IDbPos
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetClientId();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<string> GetPosId();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        Task<string> GetPosId(string deviceId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="posId"></param>
        /// <returns></returns>
        Task<string> GetServiceAreaId(string posId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceAreaId"></param>
        /// <returns></returns>
        Task<string> GetServiceAreaName(string serviceAreaId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sercieAreaId"></param>
        /// <returns></returns>
        Task<string> GetPriceLevel(string sercieAreaId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="posId"></param>
        /// <returns></returns>
        Task<Data.Pos> GetPos(string posId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="posId"></param>
        /// <returns></returns>
        Task<List<string>> GetAlternativePosIds(string posId);
    }
}