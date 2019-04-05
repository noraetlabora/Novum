using System.Collections.Generic;

namespace Novum.Database.Api
{
    /// <summary>
    /// Interface for Waiter Api
    /// </summary>
    public interface IDbWaiter
    {
        Dictionary<string, Novum.Data.Waiter> GetWaiters();
        bool ValidWaiter(string waiterId, string code);
        void Login(string deviceId, string waiterId);
    }
}