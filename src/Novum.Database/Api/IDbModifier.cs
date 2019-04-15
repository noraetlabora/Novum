using System.Collections.Generic;

namespace Novum.Database.Api
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
        Dictionary<string, Novum.Data.ModifierMenu> GetModifierMenus();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Dictionary<string, Novum.Data.Modifier> GetModifiers(string menuId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Novum.Data.MenuModifier> GetMenuModifiers();
    }
}