using System.Collections.Generic;

namespace Novum.Database.Api
{
    /// <summary>
    /// Interface for Table Api
    /// </summary>
    public interface IDbTable
    {
        Dictionary<string, Data.Table> GetTables(string department);
    }
}