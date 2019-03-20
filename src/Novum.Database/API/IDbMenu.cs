using System.Collections.Generic;

namespace Novum.Database.API
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbMenu
    {
        List<Novum.Data.Menu> GetMainMenu(string department, string menuId);
        Novum.Data.Menu GetSubMenu(string department, string menuId);
        List<Novum.Data.MenuItem> GetMenuItems(string department, string menuId);
    }
}