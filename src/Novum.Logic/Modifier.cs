using System;
using System.Collections.Generic;

namespace Novum.Logic
{
    /// <summary>
    /// 
    /// </summary>
    public class ModifierMenu
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public static List<Novum.Data.ModifierMenu> GetModifierMenus(string department)
        {
            return Novum.Database.DB.Api.Modifier.GetModifierMenus(department);
        }

        public static List<Novum.Data.Modifier> GetModifiers(string department, string menuId)
        {
            return Novum.Database.DB.Api.Modifier.GetModifiers(department, menuId);
        }
    }
}