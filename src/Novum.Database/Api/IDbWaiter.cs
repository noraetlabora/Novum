using System.Collections.Generic;

namespace Novum.Database.Api
{
    /// <summary>
    /// Interface for Waiter Api
    /// </summary>
    public interface IDbWaiter
    {
        Dictionary<string, Novum.Data.Waiter> GetWaiters();
    }
}