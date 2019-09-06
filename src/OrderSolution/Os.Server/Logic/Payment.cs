using System;
using System.Collections.Generic;
using System.Linq;

namespace Os.Server.Logic
{
    /// <summary>
    /// .
    /// </summary>
    public class Payment
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="data"></param>
        public static void PaySubTables(Nt.Data.Session session, Models.PaySubTables data)
        {
            if (data.SubTableIds == null || data.SubTableIds.Count < 1)
                throw new Exception("no sub tables");

            if (data.Payments == null || data.Payments.Count < 1)
                throw new Exception("no payments");

            if (data.SubTableIds.Count > 1)
                throw new Exception("can't handle more than one subtable");

            var tableId = data.SubTableIds[0];
            var ntOrders = Nt.Database.DB.Api.Order.GetOrders(tableId);
            var ntPaymentInformation = new Nt.Data.PaymentInformation();
            var ntPaymentMethods = new List<Nt.Data.PaymentMethod>();

            foreach (var osPayment in data.Payments)
            {
                var ntPaymentMethod = new Nt.Data.PaymentMethod();
                ntPaymentMethod.Amount = decimal.Divide((decimal)osPayment.AmountPaid, 100.0m);
                ntPaymentMethod.PaymentTypeId = osPayment.PaymentMediumId;
                ntPaymentMethod.AssignmentTypeId = "N";
                ntPaymentMethods.Add(ntPaymentMethod);
            }

            var ntPaymentResult = Nt.Database.DB.Api.Payment.Pay(session, tableId, ntOrders.Values.ToList(), ntPaymentMethods, ntPaymentInformation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="data"></param>
        public static void PayOrderLines(Nt.Data.Session session, Models.PayOrderLines data)
        {

            if (data.Payments == null || data.Payments.Count < 1)
                throw new Exception("no payments");

            if (session.CurrentTable == null || string.IsNullOrEmpty(session.CurrentTable.Id))
                throw new Exception("no open table");

            //payment Information
            var ntPaymentInformation = new Nt.Data.PaymentInformation();
            var ntPaymentMethods = new List<Nt.Data.PaymentMethod>();
            foreach (var osPayment in data.Payments)
            {
                var ntPaymentMethod = new Nt.Data.PaymentMethod();
                ntPaymentMethod.Amount = decimal.Divide((decimal)osPayment.AmountPaid, 100.0m);
                ntPaymentMethod.PaymentTypeId = osPayment.PaymentMediumId;
                ntPaymentMethod.AssignmentTypeId = "N";
                ntPaymentMethods.Add(ntPaymentMethod);
            }

            //orderlines
            var ntAllOrders = Nt.Database.DB.Api.Order.GetOrders(session.CurrentTable.Id);
            var ntOrders = new List<Nt.Data.Order>();
            foreach (var orderLineQuantity in data.PaidLines)
            {
                if (!ntAllOrders.ContainsKey(orderLineQuantity.OrderLineId))
                    throw new Exception("couldn't find orderLineId " + orderLineQuantity.OrderLineId + " in current Table " + session.CurrentTable.Id);

                if (ntAllOrders[orderLineQuantity.OrderLineId].Quantity < orderLineQuantity.Quantity)
                    throw new Exception("orderLineId " + orderLineQuantity.OrderLineId + " has just quantity " + ntAllOrders[orderLineQuantity.OrderLineId].Quantity + ", not" + orderLineQuantity.Quantity);

                var ntOrder = ntAllOrders[orderLineQuantity.OrderLineId];
                ntOrder.Quantity = (decimal)orderLineQuantity.Quantity;
                ntOrders.Add(ntOrder);
            }
            //pay
            var ntPaymentResult = Nt.Database.DB.Api.Payment.Pay(session, session.CurrentTable.Id, ntOrders, ntPaymentMethods, ntPaymentInformation);
        }
    }
}