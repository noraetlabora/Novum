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
            if (data.SubTableIds.Count <= 0)
                throw new Exception("no sub tables");

            if (data.SubTableIds.Count > 1)
                throw new Exception("can't handle more than one subtable");

            var tableId = data.SubTableIds[0];
            var ntOrders = Nt.Database.DB.Api.Order.GetOrders(tableId);
            var ntPaymentInformation = new Nt.Data.PaymentInformation();
            var ntPaymentMethods = new List<Nt.Data.PaymentMethod>();

            foreach(var osPayment in data.Payments) {
                var ntPaymentMethod = new Nt.Data.PaymentMethod();
                ntPaymentMethod.Amount = decimal.Divide((decimal)osPayment.AmountPaid, 100.0m);
                ntPaymentMethod.PaymentTypeId = osPayment.PaymentMediaId;
                ntPaymentMethod.AssignmentTypeId = "N";
                ntPaymentMethods.Add(ntPaymentMethod);
            }
            
            var ntPaymentResult = Nt.Database.DB.Api.Payment.Pay(session, tableId, ntOrders.Values.ToList(), ntPaymentMethods, ntPaymentInformation);
        }
    }
}