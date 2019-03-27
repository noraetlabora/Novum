using System.Collections.Generic;

namespace Novum.Database.API
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbMenu
    {
        Dictionary<string, Novum.Data.Menu> GetMainMenu(string department, string menuId);
        Novum.Data.Menu GetSubMenu(string department, string menuId);
    }
}