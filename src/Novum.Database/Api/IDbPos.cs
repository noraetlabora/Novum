using System.Collections.Generic;

namespace Novum.Database.Api
{
    /// <summary>
    /// Interface for Printer Api
    /// </summary>
    public interface IDbPos
    {
        string GetPosId(string deviceId);
        string GetServiceAreaId(string posId);
        string GetServiceAreaName(string serviceAreaId);
    }
}