using System.Collections.Generic;
using Novum.Data;

namespace Novum.Database.Api
{
    /// <summary>
    /// Interface for Waiter Api
    /// </summary>
    public interface IDbWaiter
    {
        Dictionary<string, Waiter> GetWaiters();
        bool ValidWaiter(Session session, string code);
        void Login(Session session);
        void Logout(Session session);
    }
}