using System;
using System.Collections.Generic;

namespace Novum.Logic
{
    /// <summary>
    /// 
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static List<Novum.Data.Menu> GetMainMenu(string department, string menuId)
        {
            return Novum.Database.DB.Api.Menu.GetMainMenu(department, menuId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static Novum.Data.Menu GetSubMenu(string department, string menuId)
        {
            return Novum.Database.DB.Api.Menu.GetSubMenu(department, menuId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static List<Novum.Data.MenuItem> GetMenuItems(string department, string menuId)
        {
            return Novum.Database.DB.Api.Menu.GetMenuItems(department, menuId);
        }

    }
}