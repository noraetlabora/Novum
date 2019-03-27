using System.Collections.Generic;

namespace Novum.Database.Api
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbArticle
    {
        Dictionary<string, Novum.Data.Article> GetArticles(string department, string menuId);
        Dictionary<string, Novum.Data.Article> GetArticles(string department);
    }
}