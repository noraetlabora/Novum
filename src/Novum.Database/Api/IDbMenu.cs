using System.Collections.Generic;

namespace Novum.Database.Api
{
    /// <summary>
    /// Interface for Menu Api
    /// </summary>
    public interface IDbMenu
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Dictionary<string, Novum.Data.Menu> GetMainMenu(string menuId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Novum.Data.Menu GetSubMenu(string menuId);
    }
}