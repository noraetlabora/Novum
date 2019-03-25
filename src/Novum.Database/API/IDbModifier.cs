using System.Collections.Generic;

namespace Novum.Database.API
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbModifier
    {
        List<Novum.Data.ModifierMenu> GetModifierMenus(string department);
        List<Novum.Data.Modifier> GetModifiers(string department, string menuId);
    }
}