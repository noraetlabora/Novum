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
        /// <param name="modifierMenuId"></param>
        /// <returns></returns>
        Dictionary<string, Nt.Data.ModifierItem> GetModifierItems(string modifierMenuId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Nt.Data.MenuItemModifierMenu> GetMenuItemModifierMenus();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="articleId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Nt.Data.Modifier GetModifier(Nt.Data.Session session, string articleId, decimal quantity);

    }
}