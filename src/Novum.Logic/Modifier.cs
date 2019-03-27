using System;
using System.Collections.Generic;

namespace Novum.Logic
{
    /// <summary>
    /// 
    /// </summary>
    public class Modifier
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public static Dictionary<string, Novum.Data.ModifierMenu> GetModifierMenus(string department)
        {
            return Novum.Database.DB.Api.Modifier.GetModifierMenus(department);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static Dictionary<string, Novum.Data.Modifier> GetModifiers(string department, string menuId)
        {
            return Novum.Database.DB.Api.Modifier.GetModifiers(department, menuId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public static List<Novum.Data.MenuModifier> GetMenuModifiers(string department)
        {
            return Novum.Database.DB.Api.Modifier.GetMenuModifiers(department);
        }
    }
}