using System.Collections.Generic;

namespace Novum.Database.Api
{
    /// <summary>
    /// Interface for Modifier Api
    /// </summary>
    public interface IDbModifier
    {
        Dictionary<string, Novum.Data.ModifierMenu> GetModifierMenus();
        Dictionary<string, Novum.Data.Modifier> GetModifiers(string menuId);
        List<Novum.Data.MenuModifier> GetMenuModifiers();
    }
}