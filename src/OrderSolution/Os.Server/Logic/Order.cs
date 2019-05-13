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

            foreach (var ntOrder in ntOrders.Values)
            {
                var osOrderLine = GetOsOrderLine(ntOrder);
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
            if (session.NotPermitted(Nt.Data.Permission.PermissionType.VoidCommitedOrder))
                throw new Exception("not permitted");
                
            if (session.CurrentTable == null)
                throw new Exception("no open table");

            //search in new/uncommited orders, when not found search in ordered/prebooked orders of current table
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
        /// <param name="orderLineId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Models.OrderLineResult Modify(Nt.Data.Session session, string orderLineId, Models.OrderLineModify data)
        {
            if (session.CurrentTable == null)
                throw new Exception("no open table");

            //search in new/uncommited orders
            var ntOrder = session.GetOrder(orderLineId);
            if (ntOrder == null)
                throw new Exception("no orderline found");

            var osOrderLineResult = new Models.OrderLineResult();
            osOrderLineResult.Modifiers = new List<Models.OrderLineResultModifier>();
            osOrderLineResult.SinglePrice = (int)decimal.Multiply(ntOrder.TotalPrice, 100.0m);
            ntOrder.ClearModifiers();

            foreach(var osOrderLineModifier2 in data.Modifiers)
            {
                var osOrderLineResultModifier = new Models.OrderLineResultModifier();
                ///////////////////////////////////
                //choices
                ///////////////////////////////////
                if (osOrderLineModifier2.Choices != null && osOrderLineModifier2.Choices.Count > 0) {
                    osOrderLineResultModifier.Id = osOrderLineModifier2.ModifierGroupId;
                    osOrderLineResultModifier.Choices = new List<Models.OrderLineResultModifierChoice>();

                    foreach(var osOrderLineModifierChoice2 in osOrderLineModifier2.Choices) 
                    {
                        var ntModifier = Nt.Database.DB.Api.Modifier.GetModifier(session, osOrderLineModifierChoice2.ModifierChoiceId, 1.0m);
                        ntOrder.AddModifier(ntModifier);
                        
                        var osOrderLineResultModifierChoice = new Models.OrderLineResultModifierChoice();
                        osOrderLineResultModifierChoice.ModifierChoiceId = ntModifier.ArticleId;
                        osOrderLineResult.SinglePrice = (int)decimal.Multiply(ntOrder.UnitPrice, 100.0m);

                        osOrderLineResultModifier.Choices.Add(osOrderLineResultModifierChoice);
                    }
                }
                ///////////////////////////////////
                //text input
                ///////////////////////////////////
                if (!string.IsNullOrEmpty(osOrderLineModifier2.TextInput)) {
                    var ntTextModifier = new Nt.Data.Modifier();
                    ntTextModifier.Name = osOrderLineModifier2.TextInput;
                    ntOrder.AddModifier(ntTextModifier);
                }
                
                ///////////////////////////////////
                //fax input
                ///////////////////////////////////
                //TODO: Modifier Fax Input
                if (!string.IsNullOrEmpty(osOrderLineModifier2.FaxInputID)) {
                    var ntFaxModifier = new Nt.Data.Modifier();
                }
            }

            return osOrderLineResult;
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

        private static Models.OrderLine GetOsOrderLine(Nt.Data.Order ntOrder) {
            var osOrderLine = new Models.OrderLine();
            osOrderLine.Id = ntOrder.Id;
            osOrderLine.ArticleId = ntOrder.ArticleId;
            osOrderLine.Quantity = (int)ntOrder.Quantity;
            osOrderLine.SinglePrice = (int)decimal.Multiply(ntOrder.UnitPrice, 100);
            osOrderLine.Status = (int)GetOsOrderLineStatus(ntOrder.Status);

            //Modifiers
            if (ntOrder.Modifiers != null && ntOrder.Modifiers.Count > 0) {
                osOrderLine.Modifiers = new List<Models.OrderLineModifier>();
                
                var osOrderLineModifier = new Models.OrderLineModifier();
                
                foreach(var ntModifier in ntOrder.Modifiers)
                {
                    osOrderLineModifier = new Models.OrderLineModifier();
                    osOrderLineModifier.Choices = new List<Models.OrderLineModifierChoice>();

                    //choice
                    if (!string.IsNullOrEmpty(ntModifier.ArticleId)) {
                        var osOrderLineModifierChoice = new Models.OrderLineModifierChoice();
                        osOrderLineModifierChoice.ModifierChoiceId = ntModifier.ArticleId;
                        osOrderLineModifier.Choices.Add(osOrderLineModifierChoice);
                        osOrderLineModifier.Id = ntModifier.MenuId + "|" + ntModifier.ArticleId;
                        osOrderLineModifier.ModifierGroupId = ntModifier.MenuId;
                    }
                    //textinput
                    else 
                    {
                        osOrderLineModifier.TextInput = ntModifier.Name;
                        ntModifier.MenuId = "text";
                        osOrderLineModifier.Id = "text|" + ntModifier.Name;
                    }

                    osOrderLine.Modifiers.Add(osOrderLineModifier);
                    
                }
            }

            return osOrderLine;
        }

        #endregion
    }
}
