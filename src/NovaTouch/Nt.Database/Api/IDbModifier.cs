using System.Collections.Generic;

namespace Nt.Database.Api
{
    /// <summary>
    /// Interface for Modifier Api
    /// </summary>
    public interface IDbModifier
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<string, Nt.Data.ModifierMenu> GetModifierMenus();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Dictionary<string, Nt.Data.Modifier> GetModifiers(string menuId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Nt.Data.MenuItemModifierMenu> GetMenuItemModifierMenus();
        
    }
}