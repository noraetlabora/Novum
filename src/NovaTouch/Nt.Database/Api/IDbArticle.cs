using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<Dictionary<string, Nt.Data.Article>> GetArticles();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        Task<bool> IsAvailable(Nt.Data.Session session, string articleId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="articleId"></param>
        /// <param name="price"></param>
        Task CheckEnteredPrice(Nt.Data.Session session, string articleId, decimal price);
    }
}