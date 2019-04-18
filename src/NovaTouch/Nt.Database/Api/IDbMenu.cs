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
        /// <param name="menuId"></param>
        /// <returns></returns>
        Dictionary<string, Nt.Data.Menu> GetMainMenu(string menuId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Nt.Data.Menu GetSubMenu(string menuId);
    }
}