using System.Collections.Generic;

namespace Novum.Database.API
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbMisc
    {
        Dictionary<string, Novum.Data.CancellationResason> GetCancellationReason(string department);
    }
}