using System.Collections.Generic;

namespace Novum.Database.Api
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbTable
    {
        Dictionary<string, Data.Table> GetTables(string department);
    }
}