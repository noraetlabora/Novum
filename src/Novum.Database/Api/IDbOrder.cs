using System.Collections.Generic;

namespace Novum.Database.Api
{
    /// <summary>
    /// Interface for Article Api
    /// </summary>
    public interface IDbOrder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        Dictionary<string, Novum.Data.Order> GetOrders(string tableId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        Novum.Data.Order GetNewOrder(Novum.Data.Session session, string articleId);
    }
}