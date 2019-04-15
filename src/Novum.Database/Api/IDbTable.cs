using System.Collections.Generic;

namespace Novum.Database.Api
{
    /// <summary>
    /// Interface for Table Api
    /// </summary>
    public interface IDbTable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<string, Data.Table> GetTables();
    }
}