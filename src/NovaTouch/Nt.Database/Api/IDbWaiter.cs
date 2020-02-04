using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<Dictionary<string, Nt.Data.Waiter>> GetWaiters();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<string> GetWaiterId(string code);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="waiterId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<bool> ValidWaiter(string waiterId, string code);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        Task Login(Nt.Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        Task Logout(Nt.Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="waiterId"></param>
        /// <returns></returns>
        Task<Dictionary<Nt.Data.Permission.PermissionType, Nt.Data.Permission>> GetPermissions(string waiterId);
    }
}