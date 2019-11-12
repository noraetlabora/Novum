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

            //order lines
            var tableId = data.SubTableIds[0];
            var ntOrders = Nt.Database.DB.Api.Order.GetOrders(tableId);

            //payment information
            var ntPaymentInformation = new Nt.Data.PaymentInformation();
            ntPaymentInformation.PrinterId = data.Printer;

            //payment methods
            var ntPaymentMethods = new List<Nt.Data.PaymentMethod>();

            foreach (var osPayment in data.Payments)
            {
                var ntPaymentMethod = GetPaymentMethod(osPayment.PaymentMediumId);
                ntPaymentMethod.Amount = decimal.Divide((decimal)osPayment.AmountPaid, 100.0m);
                ntPaymentMethods.Add(ntPaymentMethod);
            }

            //pay
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

            if (data.PaidLines == null || data.PaidLines.Count < 1)
                throw new Exception("no orderLines");

            var tableId = "";
            //check if orderlines are from one table
            foreach (var orderLine in data.PaidLines)
            {
                var orderLineId = orderLine.OrderLineId.Split('|');

                if (string.IsNullOrEmpty(tableId))
                    tableId = orderLineId[0];

                if (tableId != orderLineId[0])
                    throw new Exception("orderLines of different tables are not allowed");
            }

            //payment information
            var ntPaymentInformation = new Nt.Data.PaymentInformation();
            ntPaymentInformation.PrinterId = data.Printer;

            //payment methods
            var ntPaymentMethods = new List<Nt.Data.PaymentMethod>();
            foreach (var osPayment in data.Payments)
            {
                var ntPaymentMethod = GetPaymentMethod(osPayment.PaymentMediumId);
                ntPaymentMethod.Amount = decimal.Divide((decimal)osPayment.AmountPaid, 100.0m);
                ntPaymentMethods.Add(ntPaymentMethod);
            }

            //orderlines
            var ntAllOrders = Nt.Database.DB.Api.Order.GetOrders(tableId);
            var ntOrders = new List<Nt.Data.Order>();
            foreach (var orderLineQuantity in data.PaidLines)
            {
                if (!ntAllOrders.ContainsKey(orderLineQuantity.OrderLineId))
                    throw new Exception("couldn't find orderLineId " + orderLineQuantity.OrderLineId + " in current Table " + tableId);

                if (ntAllOrders[orderLineQuantity.OrderLineId].Quantity < orderLineQuantity.Quantity)
                    throw new Exception("orderLineId " + orderLineQuantity.OrderLineId + " has just quantity " + ntAllOrders[orderLineQuantity.OrderLineId].Quantity + ", not" + orderLineQuantity.Quantity);

                var ntOrder = ntAllOrders[orderLineQuantity.OrderLineId];
                ntOrder.Quantity = (decimal)orderLineQuantity.Quantity;
                ntOrders.Add(ntOrder);
            }

            //pay
            var ntPaymentResult = Nt.Database.DB.Api.Payment.Pay(session, tableId, ntOrders, ntPaymentMethods, ntPaymentInformation);
        }

        private static Nt.Data.PaymentMethod GetPaymentMethod(string paymentMediumId)
        {
            var ntPaymentMethod = new Nt.Data.PaymentMethod();
            var ntPaymentType = Data.GetPaymentType(paymentMediumId);

            //payment type
            if (ntPaymentType != null)
            {
                ntPaymentMethod.PaymentTypeId = ntPaymentType.Id;
                ntPaymentMethod.AssignmentTypeId = "N";
                ntPaymentMethod.Program = ntPaymentType.Program;
                ntPaymentMethod.Name = ntPaymentType.Name;
                return ntPaymentMethod;
            }

            //assignment type
            var ntAssignmentType = Data.GetAssignmentType(paymentMediumId);
            if (ntAssignmentType != null)
            {
                ntPaymentMethod.PaymentTypeId = ntAssignmentType.Id;
                ntPaymentMethod.AssignmentTypeId = ntAssignmentType.Id;
                ntPaymentMethod.Program = "";
                ntPaymentMethod.Name = ntAssignmentType.Name;
                return ntPaymentMethod;
            }

            //no valid payment or assignment type
            throw new Exception("payment method must be a payment type or an assignment type: " + paymentMediumId.ToString());
        }
    }
}