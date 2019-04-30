using System;
using System.Collections.Generic;

namespace Os.Server.Logic
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
        public static List<Models.OrderLine> GetOrderLines(Nt.Data.Session session, string subTableId)
        {
            var osOrderLines = new List<Models.OrderLine>();
            var ntOrders = Nt.Database.DB.Api.Order.GetOrders(subTableId);

            //orderd or prebooked orderlines
            foreach (var ntOrder in ntOrders.Values)
            {
                var osOrderLine = new Models.OrderLine();
                osOrderLine.Id = ntOrder.Id;
                osOrderLine.ArticleId = ntOrder.ArticleId;
                osOrderLine.Quantity = (int)ntOrder.Quantity;
                osOrderLine.SinglePrice = (int)decimal.Multiply(ntOrder.UnitPrice, 100);
                osOrderLine.Status = GetOsOrderLineStatus(ntOrder.Status);
                osOrderLines.Add(osOrderLine);
            }

            //new orders
            foreach (var ntOrder in session.GetOrders())
            {
                var osOrderLine = new Models.OrderLine();
                osOrderLine.Id = ntOrder.Id;
                osOrderLine.ArticleId = ntOrder.ArticleId;
                osOrderLine.Quantity = (int)ntOrder.Quantity;
                osOrderLine.SinglePrice = (int)decimal.Multiply(ntOrder.UnitPrice, 100);
                osOrderLine.Status = GetOsOrderLineStatus(ntOrder.Status);
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
        public static Models.OrderLineResult Add(Nt.Data.Session session, string subTableId, Models.OrderLineAdd data)
        {
            if (session.CurrentTable == null)
                throw new Exception("no open table");

            var orderLineResult = new Models.OrderLineResult();

            if (data.EnteredPrice != null && data.EnteredPrice != 0)
                Nt.Database.DB.Api.Article.CheckEnteredPrice(session, data.ArticleId, decimal.Multiply((decimal)data.EnteredPrice, 100.0m));
            var order = Nt.Database.DB.Api.Order.GetNewOrder(session, data.ArticleId);
            order.Quantity = (decimal)data.Quantity;
            session.AddOrder(order);

            orderLineResult.Id = order.Id;
            orderLineResult.SinglePrice = (int)decimal.Multiply(order.UnitPrice, 100.0m);

            return orderLineResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="orderLineId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Models.OrderLineVoidResult Void(Nt.Data.Session session, string orderLineId, Models.OrderLineVoid data)
        {
            if (session.CurrentTable == null)
                throw new Exception("no open table");

            //search in new orders, when not found search in ordered/prebooked orders of current table
            var ntOrder = session.GetOrder(orderLineId);
            if (ntOrder == null)
            {
                var ntOrders = Nt.Database.DB.Api.Order.GetOrders(session.CurrentTable.Id);
                if (!ntOrders.ContainsKey(orderLineId))
                    throw new Exception("no orderline found");
                ntOrder = ntOrders[orderLineId];
            }

            // set voidResult
            var voidResult = new Models.OrderLineVoidResult();
            voidResult.OrderLineId = orderLineId;
            voidResult.SinglePrice = (int)decimal.Multiply(ntOrder.UnitPrice, 100);
            if (data.Quantity >= ntOrder.Quantity)
                voidResult.Quantity = 0;
            else
                voidResult.Quantity = ((int)ntOrder.Quantity - data.Quantity);

            switch (ntOrder.Status)
            {
                case Nt.Data.Order.OrderStatus.NewOrder:
                    var price = decimal.Multiply((decimal)data.Quantity, ntOrder.UnitPrice);
                    Nt.Database.DB.Api.Order.VoidNewOrder(session, session.CurrentTable.Id, ntOrder.ArticleId, (decimal)data.Quantity, price, ntOrder.AssignmentTypeId, "");
                    break;
                case Nt.Data.Order.OrderStatus.Prebooked:
                    Nt.Database.DB.Api.Order.VoidPrebookedOrder(session, session.CurrentTable.Id, (decimal)data.Quantity, ntOrder.SequenceNumber, "");
                    break;
                case Nt.Data.Order.OrderStatus.Ordered:
                    Nt.Database.DB.Api.Order.VoidOrderedOrder(session, session.CurrentTable.Id, ntOrder, (decimal)data.Quantity, data.CancellationReasonId, "");
                    break;
            }

            session.VoidOrder(ntOrder.Id, (decimal)data.Quantity);
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
        public static Models.OrderLineResult Modify(Nt.Data.Session session, string subTableId, Models.OrderLineModify data)
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

        private static Models.OrderLine.OrderLineStatus GetOsOrderLineStatus(Nt.Data.Order.OrderStatus ntOrderStatus)
        {
            switch (ntOrderStatus)
            {
                case Nt.Data.Order.OrderStatus.Ordered:
                    return Models.OrderLine.OrderLineStatus.CommittedEnum;
                case Nt.Data.Order.OrderStatus.NewOrder:
                    return Models.OrderLine.OrderLineStatus.OrderedEnum;
                default:
                    return Models.OrderLine.OrderLineStatus.UnknownEnum;
            }
        }

        #endregion
    }
}
