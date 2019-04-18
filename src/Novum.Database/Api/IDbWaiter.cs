using System.Collections.Generic;
using Novum.Data;

namespace Novum.Database.Api
{
    /// <summary>
    /// Interface for Waiter Api
    /// </summary>
    public interface IDbWaiter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<string, Waiter> GetWaiters();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="waiterId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        bool ValidWaiter(string waiterId, string code);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        void Login(Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        void Logout(Session session);
    }
}
