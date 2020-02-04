using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nt.Database.Api
{
    /// <summary>
    /// Interface for Menu Api
    /// </summary>
    public interface IDbMenu
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<string, Nt.Data.Menu>> GetMenus();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="posId"></param>
        /// <returns></returns>
        Task<string> GetMenuId(string posId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Task<Dictionary<string, Nt.Data.Menu>> GetMainMenus(string menuId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<Nt.Data.MenuItem>> GetMenuItems();

    }
}