using System.Collections.Generic;

namespace Novum.Database.API
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbMenu
    {
        List<string> GetCategories(string department, string menuId);
    }
}