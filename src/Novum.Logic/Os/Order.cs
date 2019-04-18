using System;
using System.Collections.Generic;
using Novum.Data;
using Novum.Data.Os;
using Novum.Database;

namespace Novum.Logic.Os
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
        public static List<OrderLine> GetOrderLines(Session session, string subTableId)
        {
            var osOrderLines = new List<OrderLine>();
            var novOrders = DB.Api.Order.GetOrders(subTableId);

            //orderd or prebooked orderlines
            foreach (var novOrder in novOrders.Values)
            {
                var osOrderLine = new OrderLine();
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
                var osOrderLine = new OrderLine();
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
        public static OrderLineResult Add(Session session, string subTableId, OrderLineAdd data)
        {
            if (session.CurrentTable == null)
                throw new Exception("no open table");

            var orderLineResult = new OrderLineResult();

            if (data.EnteredPrice != 0)
                DB.Api.Article.CheckEnteredPrice(session, data.ArticleId, decimal.Multiply((decimal)data.EnteredPrice, 100));
            var order = DB.Api.Order.GetNewOrder(session, data.ArticleId);
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
        public static OrderLineVoidResult Void(Session session, string orderLineId, OrderLineVoid data)
        {
            if (session.CurrentTable == null)
                throw new Exception("no open table");

            var order = session.GetOrder(orderLineId);
            if (order == null)
                throw new Exception("no orderLine found");

            // set voidResult
            var voidResult = new OrderLineVoidResult();
            voidResult.OrderLineId = orderLineId;
            voidResult.SinglePrice = (int)decimal.Multiply(order.UnitPrice, 100);
            if (data.Quantity >= order.Quantity)
                voidResult.Quantity = 0;
            else
                voidResult.Quantity = ((int)order.Quantity - data.Quantity);

            // OrderStatus.NewOrder
            if (order.Status == Novum.Data.Order.OrderStatus.NewOrder)
            {
                var price = decimal.Multiply((decimal)data.Quantity, order.UnitPrice);
                DB.Api.Order.VoidNewOrder(session, session.CurrentTable.Id, order.ArticleId, (decimal)data.Quantity, price, "");
            }
            // OrderStatus.Prebooked
            else if (order.Status == Novum.Data.Order.OrderStatus.Prebooked)
            {
                DB.Api.Order.VoidPrebookedOrder(session, session.CurrentTable.Id, (decimal)data.Quantity, order.SequenceNumber, "");
            }
            // OrderStatus.Ordered
            else
            {
                DB.Api.Order.VoidOrderedOrder(session, session.CurrentTable.Id, order, (decimal)voidResult.Quantity, data.CancellationReasonId, "");
            }

            session.VoidOrder(order.Id, (decimal)data.Quantity);
            DB.Api.Table.UnlockTable(session, session.CurrentTable.Id);
            return voidResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="subTableId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static OrderLineResult Modify(Session session, string subTableId, OrderLineModify data)
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
        public static void CancelOrder(Session session, string tableId)
        {
            if (session.CurrentTable == null)
                throw new Exception("no open table");

            DB.Api.Table.UnlockTable(session, session.CurrentTable.Id);
            session.ClearOrders();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        public static void FinalizeOrder(Session session, string tableId)
        {
            if (session.CurrentTable == null)
                throw new Exception("no open table");
            DB.Api.Order.FinilizeOrder(session, tableId);
            DB.Api.Table.UnlockTable(session, session.CurrentTable.Id);
            session.ClearOrders();
        }

        #endregion

        #region private static methods

        private static OrderLine.OrderLineStatus GetOsOrderLineStatus(Novum.Data.Order.OrderStatus novOrderStatus)
        {
            switch (novOrderStatus)
            {
                case Novum.Data.Order.OrderStatus.Ordered:
                    return OrderLine.OrderLineStatus.CommittedEnum;
                case Novum.Data.Order.OrderStatus.NewOrder:
                    return OrderLine.OrderLineStatus.OrderedEnum;
                default:
                    return OrderLine.OrderLineStatus.UnknownEnum;
            }
        }

        #endregion
    }
}
