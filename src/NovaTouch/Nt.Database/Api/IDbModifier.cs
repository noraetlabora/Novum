using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<Dictionary<string, Nt.Data.ModifierMenu>> GetModifierMenus();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modifierMenuId"></param>
        /// <returns></returns>
        Task<Dictionary<string, Nt.Data.ModifierItem>> GetModifierItems(string modifierMenuId = "");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<Nt.Data.MenuItemModifierMenu>> GetMenuItemModifierMenus();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="articleId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task<Nt.Data.Modifier> GetModifier(Nt.Data.Session session, string articleId, decimal quantity);

    }
}