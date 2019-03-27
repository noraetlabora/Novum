using System.Collections.Generic;

namespace Novum.Database.Api
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbModifier
    {
        Dictionary<string, Novum.Data.ModifierMenu> GetModifierMenus(string department);
        Dictionary<string, Novum.Data.Modifier> GetModifiers(string department, string menuId);
        List<Novum.Data.MenuModifier> GetMenuModifiers(string department);
    }
}