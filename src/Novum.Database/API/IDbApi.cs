using System;

namespace Novum.Database.API
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbApi
    {
        Database.API.IDbMisc Misc { get; }
        Database.API.IDbTable Tables { get; }
        Database.API.IDbUser User { get; }
        Database.API.IDbMenu Menu { get; }
    }
}