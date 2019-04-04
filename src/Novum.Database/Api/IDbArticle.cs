using System.Collections.Generic;

namespace Novum.Database.Api
{
    /// <summary>
    /// Interface for Article Api
    /// </summary>
    public interface IDbArticle
    {
        Dictionary<string, Novum.Data.Article> GetArticles(string menuId);
        Dictionary<string, Novum.Data.Article> GetArticles();
    }
}