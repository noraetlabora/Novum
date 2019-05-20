using System.Collections.Generic;

namespace Nt.Database.Api
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
        Dictionary<string, Nt.Data.Order> GetOrders(string tableId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        Nt.Data.Order GetNewOrder(Nt.Data.Session session, string articleId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <param name="order"></param>
        /// <param name="newQuantity"></param>
        /// <param name="cancellationReasonId"></param>
        /// <param name="authorizingWaiterId"></param>
        void VoidOrderedOrder(Nt.Data.Session session, string tableId, Nt.Data.Order order, decimal newQuantity, string cancellationReasonId, string authorizingWaiterId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <param name="articleId"></param>
        /// <param name="voidQuantity"></param>
        /// <param name="voidPrice"></param>
        /// <param name="assignmentTypeId"></param>
        /// <param name="authorizingWaiterId"></param>
        void VoidNewOrder(Nt.Data.Session session, string tableId, string articleId, decimal voidQuantity, decimal voidPrice, string assignmentTypeId, string authorizingWaiterId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <param name="orderSequenceNumber"></param>
        /// <param name="authorizingWaiterId"></param>
        /// <param name="voidQuantity"></param>
        void VoidPrebookedOrder(Nt.Data.Session session, string tableId, decimal voidQuantity, string orderSequenceNumber, string authorizingWaiterId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        void FinalizeOrder(Nt.Data.Session session, string tableId);

    }
}