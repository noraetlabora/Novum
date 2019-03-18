using System;

namespace Novum.Database.API
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbApi
    {
        Database.API.IDbMisc Misc { get; }
        Database.API.IDbTables Tables { get; }
        Database.API.IDbUser User { get; }
    }
}