using System.Collections.Generic;

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
        Dictionary<string, Nt.Data.Menu> GetMenus();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="posId"></param>
        /// <returns></returns>
        string GetMenuId(string posId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Dictionary<string, Nt.Data.Menu> GetMainMenus(string menuId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Nt.Data.MenuItem> GetMenuItems();

    }
}