using System;
using System.Collections.Generic;

namespace Os.Logic
{
    /// <summary>
    /// 
    /// </summary>
    public class Order
    {

        #region public static methdods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="subTableId"></param>
        /// <returns></returns>
        public static List<Os.Data.OrderLine> GetOrderLines(Nt.Data.Session session, string subTableId)
        {
            var osOrderLines = new List<Os.Data.OrderLine>();
            var novOrders = Nt.Database.DB.Api.Order.GetOrders(subTableId);

            //orderd or prebooked orderlines
            foreach (var novOrder in novOrders.Values)
            {
                var osOrderLine = new Os.Data.OrderLine();
                osOrderLine.Id = novOrder.Id;
                osOrderLine.ArticleId = novOrder.ArticleId;
                osOrderLine.Quantity = (int)novOrder.Quantity;
                osOrderLine.SinglePrice = (int)decimal.Multiply(novOrder.UnitPrice, 100);
                osOrderLine.Status = GetOsOrderLineStatus(novOrder.Status);
                osOrderLines.Add(osOrderLine);
            }

            //new orders
            foreach (var novOrder in session.GetOrders())
            {
                var osOrderLine = new Os.Data.OrderLine();
                osOrderLine.Id = novOrder.Id;
                osOrderLine.ArticleId = novOrder.ArticleId;
                osOrderLine.Quantity = (int)novOrder.Quantity;
                osOrderLine.SinglePrice = (int)decimal.Multiply(novOrder.UnitPrice, 100);
                osOrderLine.Status = GetOsOrderLineStatus(novOrder.Status);
                osOrderLines.Add(osOrderLine);
            }

            return osOrderLines;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="subTableId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Os.Data.OrderLineResult Add(Nt.Data.Session session, string subTableId, Os.Data.OrderLineAdd data)
        {
            if (session.CurrentTable == null)
                throw new Exception("no open table");

            var orderLineResult = new Os.Data.OrderLineResult();

            if (data.EnteredPrice != 0)
                Nt.Database.DB.Api.Article.CheckEnteredPrice(session, data.ArticleId, decimal.Multiply((decimal)data.EnteredPrice, 100));
            var order = Nt.Database.DB.Api.Order.GetNewOrder(session, data.ArticleId);
            order.Quantity = (decimal)data.Quantity;
            session.AddOrder(order);

            orderLineResult.Id = order.Id;
            orderLineResult.SinglePrice = (int)decimal.Multiply(order.UnitPrice, 100);

            return orderLineResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="orderLineId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Os.Data.OrderLineVoidResult Void(Nt.Data.Session session, string orderLineId, Os.Data.OrderLineVoid data)
        {
            if (session.CurrentTable == null)
                throw new Exception("no open table");

            var order = session.GetOrder(orderLineId);
            if (order == null)
                throw new Exception("no orderLine found");

            // set voidResult
            var voidResult = new Os.Data.OrderLineVoidResult();
            voidResult.OrderLineId = orderLineId;
            voidResult.SinglePrice = (int)decimal.Multiply(order.UnitPrice, 100);
            if (data.Quantity >= order.Quantity)
                voidResult.Quantity = 0;
            else
                voidResult.Quantity = ((int)order.Quantity - data.Quantity);

            // OrderStatus.NewOrder
            if (order.Status == Nt.Data.Order.OrderStatus.NewOrder)
            {
                var price = decimal.Multiply((decimal)data.Quantity, order.UnitPrice);
                Nt.Database.DB.Api.Order.VoidNewOrder(session, session.CurrentTable.Id, order.ArticleId, (decimal)data.Quantity, price, "");
            }
            // OrderStatus.Prebooked
            else if (order.Status == Nt.Data.Order.OrderStatus.Prebooked)
            {
                Nt.Database.DB.Api.Order.VoidPrebookedOrder(session, session.CurrentTable.Id, (decimal)data.Quantity, order.SequenceNumber, "");
            }
            // OrderStatus.Ordered
            else
            {
                Nt.Database.DB.Api.Order.VoidOrderedOrder(session, session.CurrentTable.Id, order, (decimal)voidResult.Quantity, data.CancellationReasonId, "");
            }

            session.VoidOrder(order.Id, (decimal)data.Quantity);
            Nt.Database.DB.Api.Table.UnlockTable(session, session.CurrentTable.Id);
            return voidResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="subTableId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Os.Data.OrderLineResult Modify(Nt.Data.Session session, string subTableId, Os.Data.OrderLineModify data)
        {
            if (session.CurrentTable == null)
                throw new Exception("no open table");

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        public static void CancelOrder(Nt.Data.Session session, string tableId)
        {
            if (session.CurrentTable == null)
                throw new Exception("no open table");

            Nt.Database.DB.Api.Table.UnlockTable(session, session.CurrentTable.Id);
            session.ClearOrders();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        public static void FinalizeOrder(Nt.Data.Session session, string tableId)
        {
            if (session.CurrentTable == null)
                throw new Exception("no open table");
            Nt.Database.DB.Api.Order.FinilizeOrder(session, tableId);
            Nt.Database.DB.Api.Table.UnlockTable(session, session.CurrentTable.Id);
            session.ClearOrders();
        }

        #endregion

        #region private static methods

        private static Os.Data.OrderLine.OrderLineStatus GetOsOrderLineStatus(Nt.Data.Order.OrderStatus novOrderStatus)
        {
            switch (novOrderStatus)
            {
                case Nt.Data.Order.OrderStatus.Ordered:
                    return Os.Data.OrderLine.OrderLineStatus.CommittedEnum;
                case Nt.Data.Order.OrderStatus.NewOrder:
                    return Os.Data.OrderLine.OrderLineStatus.OrderedEnum;
                default:
                    return Os.Data.OrderLine.OrderLineStatus.UnknownEnum;
            }
        }

        #endregion
    }
}