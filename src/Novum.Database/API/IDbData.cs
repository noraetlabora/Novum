using System.Collections.Generic;

namespace Novum.Database.API
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbMisc
    {
        List<Novum.Data.CancellationResason> GetCancellationReason(string department);
    }
}