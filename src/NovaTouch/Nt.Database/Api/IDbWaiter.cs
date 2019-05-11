using System.Collections.Generic;

namespace Nt.Database.Api
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
        Dictionary<string, Nt.Data.Waiter> GetWaiters();

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
        void Login(Nt.Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        void Logout(Nt.Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="waiterId"></param>
        /// <returns></returns>
        Dictionary<Nt.Data.Permission.PermissionType, Nt.Data.Permission> GetPermissions(string waiterId);
    }
}