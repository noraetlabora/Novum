using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public static async Task<List<Models.OrderLine>> GetOrderLines(Nt.Data.Session session, string subTableId)
        {
            var osOrderLines = new List<Models.OrderLine>();
            var ntOrders = await Nt.Database.DB.Api.Order.GetOrders(subTableId);
            //committed
            foreach (var ntOrder in ntOrders.Values)
            {
                var osOrderLine = GetOsOrderLine(ntOrder);
                osOrderLines.Add(osOrderLine);
            }
            //uncommited
            foreach (var ntOrder in session.GetOrders())
            {
                if (ntOrder.TableId.Equals(subTableId))
                {
                    var osOrderLine = GetOsOrderLine(ntOrder);
                    osOrderLines.Add(osOrderLine);
                }
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
        public static async Task<Models.OrderLineResult> Add(Nt.Data.Session session, string subTableId, Models.OrderLineAdd data)
        {
            if (session.CurrentTable == null)
                throw new Exception(Resources.Dictionary.GetString("Table_NotFound"));

            var osOrderLineResult = new Models.OrderLineResult();

            if (data.EnteredPrice != null)
                await Nt.Database.DB.Api.Article.CheckEnteredPrice(session, data.ArticleId, decimal.Divide((decimal)data.EnteredPrice, 100.0m));

            var ntOrder = await Nt.Database.DB.Api.Order.GetNewOrder(session, data.ArticleId);
            ntOrder.Quantity = (decimal)data.Quantity;

            if (data.EnteredPrice != null)
                ntOrder.UnitPrice = decimal.Divide((decimal)data.EnteredPrice, 100.0m);

            if (data.Modifiers != null)
                await SetModifiers(session, ntOrder, data.Modifiers);

            session.AddOrder(ntOrder);
            osOrderLineResult.Id = ntOrder.Id;
            osOrderLineResult.SinglePrice = (int)decimal.Multiply(ntOrder.UnitPrice, 100.0m);

            return osOrderLineResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="orderLineId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<Models.OrderLineVoidResult> Void(Nt.Data.Session session, string orderLineId, Models.OrderLineVoid data)
        {
            if (session.CurrentTable == null)
                throw new Exception(Resources.Dictionary.GetString("Table_NotOpen"));

            //search in new/uncommited orders, when not found search in ordered/prebooked orders of current table
            var ntOrder = session.GetOrder(orderLineId);
            if (ntOrder == null)
            {
                var ntOrders = await Nt.Database.DB.Api.Order.GetOrders(session.CurrentTable.Id);
                if (!ntOrders.ContainsKey(orderLineId))
                    throw new Exception(Resources.Dictionary.GetString("Order_NotFound"));
                ntOrder = ntOrders[orderLineId];
            }

            switch (ntOrder.Status)
            {
                case Nt.Data.Order.OrderStatus.NewOrder:
                    if (session.NotPermitted(Nt.Data.Permission.PermissionType.CancelUnconfirmedOrder))
                    {
                        Nt.Logging.Log.Server.Error("void new order is not allowed: " + orderLineId);
                        throw new Exception(Resources.Dictionary.GetString("Order_VoidNotAllowed"));
                    }

                    var price = decimal.Multiply((decimal)data.Quantity, ntOrder.UnitPrice);
                    await Nt.Database.DB.Api.Order.VoidNewOrder(session, session.CurrentTable.Id, ntOrder.ArticleId, (decimal)data.Quantity, price, ntOrder.AssignmentTypeId, "");
                    session.VoidOrder(orderLineId, (decimal)data.Quantity);
                    break;
                case Nt.Data.Order.OrderStatus.Prebooked:
                    if (session.NotPermitted(Nt.Data.Permission.PermissionType.CancelUnconfirmedOrder))
                    {
                        Nt.Logging.Log.Server.Error("void prebooked order is not allowed: " + orderLineId);
                        throw new Exception(Resources.Dictionary.GetString("Order_VoidNotAllowed"));
                    }

                    await Nt.Database.DB.Api.Order.VoidPrebookedOrder(session, session.CurrentTable.Id, (decimal)data.Quantity, ntOrder.SequenceNumber, "");
                    break;
                case Nt.Data.Order.OrderStatus.Ordered:
                    if (session.NotPermitted(Nt.Data.Permission.PermissionType.CancelConfirmedOrder))
                    {
                        Nt.Logging.Log.Server.Error("void confirmed order is not allowed: " + orderLineId);
                        throw new Exception(Resources.Dictionary.GetString("Order_VoidNotAllowed"));
                    }

                    await Nt.Database.DB.Api.Order.VoidOrderedOrder(session, session.CurrentTable.Id, ntOrder, (decimal)data.Quantity, data.CancellationReasonId, "");
                    break;
            }

            var voidResult = new Models.OrderLineVoidResult();
            voidResult.OrderLineId = orderLineId;
            voidResult.SinglePrice = (int)decimal.Multiply(ntOrder.UnitPrice, 100.0m);
            if (data.Quantity >= ntOrder.Quantity)
                voidResult.Quantity = 0;
            else
                voidResult.Quantity = ((int)ntOrder.Quantity - data.Quantity);

            return voidResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="orderLineId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<Models.OrderLineResult> Modify(Nt.Data.Session session, string orderLineId, Models.OrderLineModify data)
        {
            if (session.CurrentTable == null)
                throw new Exception(Resources.Dictionary.GetString("Table_NotOpen"));

            if (!session.ContainsOrder(orderLineId))
                throw new Exception(Resources.Dictionary.GetString("Order_NotFound"));

            var ntOrder = session.GetOrder(orderLineId);

            var osOrderLineResult = new Models.OrderLineResult();
            osOrderLineResult.Modifiers = await SetModifiers(session, ntOrder, data.Modifiers);
            osOrderLineResult.SinglePrice = (int)decimal.Multiply(ntOrder.UnitPrice, 100.0m);
            osOrderLineResult.Id = ntOrder.Id;

            return osOrderLineResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        public static async Task CancelOrder(Nt.Data.Session session, string tableId)
        {
            if (session.CurrentTable == null)
                throw new Exception(Resources.Dictionary.GetString("Table_NotOpen"));

            await Nt.Database.DB.Api.Table.UnlockTable(session, session.CurrentTable.Id);
            session.ClearOrders();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        public static async Task FinalizeOrder(Nt.Data.Session session, string tableId)
        {
            var subTableOrders = new List<Nt.Data.Order>();
            var lastSubTableId = "";
            //
            foreach (var order in session.GetOrders())
            {
                if (order.TableId != lastSubTableId)
                {
                    await Nt.Database.DB.Api.Order.FinalizeOrder(session, subTableOrders, lastSubTableId);
                    subTableOrders = new List<Nt.Data.Order>();
                }

                subTableOrders.Add(order);
                lastSubTableId = order.TableId;
            }
            //
            await Nt.Database.DB.Api.Order.FinalizeOrder(session, subTableOrders, lastSubTableId);
            await Nt.Database.DB.Api.Table.UnlockTable(session, session.CurrentTable.Id);
            session.ClearOrders();
            Image.RemoveImages(session);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="orderLineId"></param>
        /// <param name="data"></param>
        public static Models.OrderLineSplitResult Split(Nt.Data.Session session, string orderLineId, Models.OrderLineSplit data)
        {
            var osOrderLineSplitResult = new Models.OrderLineSplitResult();

            if (!session.ContainsOrder(orderLineId))
                throw new Exception(Resources.Dictionary.GetString("Order_NotFound"));

            var ntSplitOrderId = session.SplitOrder(orderLineId, (decimal)data.Quantity);
            //
            var ntOrder = session.GetOrder(orderLineId);
            var osOrderLine = GetOsOrderLine(ntOrder);
            osOrderLineSplitResult.OriginalOl.Id = osOrderLine.Id;
            osOrderLineSplitResult.OriginalOl.ArticleId = osOrderLine.ArticleId;
            osOrderLineSplitResult.OriginalOl.Quantity = osOrderLine.Quantity;
            osOrderLineSplitResult.OriginalOl.SingelPrice = osOrderLine.SinglePrice;
            osOrderLineSplitResult.OriginalOl.Modifiers = osOrderLine.Modifiers;
            //
            var ntSplitOrder = session.GetOrder(ntSplitOrderId);
            var osSplitOrderLine = GetOsOrderLine(ntSplitOrder);
            osOrderLineSplitResult.SplittedOl.Id = osSplitOrderLine.Id;
            osOrderLineSplitResult.SplittedOl.ArticleId = osSplitOrderLine.ArticleId;
            osOrderLineSplitResult.SplittedOl.Quantity = osSplitOrderLine.Quantity;
            osOrderLineSplitResult.SplittedOl.SingelPrice = osSplitOrderLine.SinglePrice;
            osOrderLineSplitResult.SplittedOl.Modifiers = osSplitOrderLine.Modifiers;
            //
            return osOrderLineSplitResult;
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

        private static Models.OrderLine GetOsOrderLine(Nt.Data.Order ntOrder)
        {
            var osOrderLine = new Models.OrderLine();
            osOrderLine.Id = ntOrder.Id;
            osOrderLine.ArticleId = ntOrder.ArticleId;
            osOrderLine.Quantity = (int)ntOrder.Quantity;
            osOrderLine.SinglePrice = (int)decimal.Multiply(ntOrder.UnitPrice, 100.0m);
            osOrderLine.Status = GetOsOrderLineStatus(ntOrder.Status);

            //Modifiers
            if (ntOrder.Modifiers != null && ntOrder.Modifiers.Count > 0)
            {
                osOrderLine.Modifiers = new List<Models.OrderLineModifier>();

                var osOrderLineModifier = new Models.OrderLineModifier();

                foreach (var ntModifier in ntOrder.Modifiers)
                {
                    osOrderLineModifier = new Models.OrderLineModifier();
                    osOrderLineModifier.Choices = new List<Models.OrderLineModifierChoice>();

                    //choice
                    if (!string.IsNullOrEmpty(ntModifier.ArticleId))
                    {
                        var osOrderLineModifierChoice = new Models.OrderLineModifierChoice();
                        osOrderLineModifierChoice.ModifierChoiceId = ntModifier.ArticleId;
                        osOrderLineModifier.Choices.Add(osOrderLineModifierChoice);
                        osOrderLineModifier.ModifierGroupId = ntModifier.MenuId;
                    }
                    //textinput
                    else if (!string.IsNullOrEmpty(ntModifier.Name))
                    {
                        osOrderLineModifier.TextInput = ntModifier.Name;
                        ntModifier.MenuId = "text";
                    }

                    osOrderLine.Modifiers.Add(osOrderLineModifier);
                }
            }

            return osOrderLine;
        }

        private static async Task<List<Models.OrderLineResultModifier>> SetModifiers(Nt.Data.Session session, Nt.Data.Order ntOrder, List<Models.OrderLineModifier> osOrderLineModifiers)
        {
            var osOrderLineResultModifiers = new List<Models.OrderLineResultModifier>();
            ntOrder.ClearModifiers();

            foreach (var osOrderLineModifier in osOrderLineModifiers)
            {
                var osOrderLineResultModifier = new Models.OrderLineResultModifier();
                ///////////////////////////////////
                //choices
                ///////////////////////////////////
                if (osOrderLineModifier.Choices != null && osOrderLineModifier.Choices.Count > 0)
                {
                    osOrderLineResultModifier.Choices = new List<Models.OrderLineResultModifierChoice>();

                    foreach (var osOrderLineModifierChoice in osOrderLineModifier.Choices)
                    {
                        var ntModifier = await Nt.Database.DB.Api.Modifier.GetModifier(session, osOrderLineModifierChoice.ModifierChoiceId, 1.0m);
                        ntModifier.MenuId = osOrderLineModifier.ModifierGroupId;
                        ntOrder.AddModifier(ntModifier);

                        var osOrderLineResultModifierChoice = new Models.OrderLineResultModifierChoice();
                        osOrderLineResultModifierChoice.ModifierChoiceId = ntModifier.ArticleId;
                        osOrderLineResultModifier.Choices.Add(osOrderLineResultModifierChoice);
                    }
                    osOrderLineResultModifiers.Add(osOrderLineResultModifier);
                }
                ///////////////////////////////////
                //text input
                ///////////////////////////////////
                if (!string.IsNullOrEmpty(osOrderLineModifier.TextInput))
                {
                    var ntTextModifier = new Nt.Data.Modifier();
                    ntTextModifier.Name = osOrderLineModifier.TextInput;
                    ntOrder.AddModifier(ntTextModifier);
                }

                ///////////////////////////////////
                //fax input
                ///////////////////////////////////
                if (!string.IsNullOrEmpty(osOrderLineModifier.FaxInputID))
                {
                    var ntFaxModifier = new Nt.Data.Modifier();
                    ntFaxModifier.Image = Image.GetImage(osOrderLineModifier.FaxInputID);
                    ntOrder.AddModifier(ntFaxModifier);
                }
            }

            return osOrderLineResultModifiers;
        }

        #endregion


    }
}
