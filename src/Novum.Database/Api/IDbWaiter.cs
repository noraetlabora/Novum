using System.Collections.Generic;

namespace Novum.Database.Api
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbWaiter
    {
        Dictionary<string, Novum.Data.Waiter> GetWaiters(string department);
    }
}