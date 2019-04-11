using System.Collections.Generic;

namespace Novum.Database.Api
{
    /// <summary>
    /// Interface for Article Api
    /// </summary>
    public interface IDbOrder
    {
        Dictionary<string, Novum.Data.Order> GetOrders(string tableId);
        Novum.Data.Order GetNewOrder(Novum.Data.Session session, string articleId);
    }
}