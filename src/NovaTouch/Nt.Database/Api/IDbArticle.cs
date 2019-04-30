using System.Collections.Generic;

namespace Nt.Database.Api
{
    /// <summary>
    /// Interface for Article Api
    /// </summary>
    public interface IDbArticle
    {
        // / <summary>
        // / 
        // / </summary>
        // / <param name="menuId"></param>
        // / <returns></returns>
        //Dictionary<string, Nt.Data.Article> GetArticles(string menuId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<string, Nt.Data.Article> GetArticles();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        bool IsAvailable(Nt.Data.Session session, string articleId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="articleId"></param>
        /// <param name="price"></param>
        void CheckEnteredPrice(Nt.Data.Session session, string articleId, decimal price);
    }
}