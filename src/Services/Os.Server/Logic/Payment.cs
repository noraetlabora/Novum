using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nt.Data;

namespace Os.Server.Logic
{
    /// <summary>
    /// .
    /// </summary>
    public class Payment
    {

        public static Dictionary<string, Nt.Data.PaymentMethod> AuthData = new Dictionary<string, PaymentMethod>();

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
            var ntOrders = Task.Run(async () => await Nt.Database.DB.Api.Order.GetOrders(tableId)).Result;
            //payment information
            var ntPaymentInformation = new Nt.Data.PaymentInformation();
            ntPaymentInformation.PrinterId = data.Printer;
            //payment methods
            List<PaymentMethod> ntPaymentMethods = GetNtPaymentMethods(data.Payments, data.Tip);

            //pay
            var ntPaymentResult = Task.Run(async () => await Nt.Database.DB.Api.Payment.Pay(session, tableId, ntOrders.Values.ToList(), ntPaymentMethods, ntPaymentInformation));
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
            List<PaymentMethod> ntPaymentMethods = GetNtPaymentMethods(data.Payments, data.Tip);
            //orders
            List<Nt.Data.Order> ntOrders = GetNtOrders(data, tableId);
            //pay
            var ntPaymentResult = Nt.Database.DB.Api.Payment.Pay(session, tableId, ntOrders, ntPaymentMethods, ntPaymentInformation);
        }

        private static List<Nt.Data.Order> GetNtOrders(Models.PayOrderLines data, string tableId)
        {
            //orderlines
            var ntAllOrders = Task.Run(async () => await Nt.Database.DB.Api.Order.GetOrders(tableId)).Result;
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

            return ntOrders;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Models.PreAuthResult PreAuthorize(Session session, Models.PreAuthData data)
        {

            var preAuthResult = new Models.PreAuthResult();
            var preAuthMedium = new Models.PreAuthMedium();
            var paymentMethod = new Nt.Data.PaymentMethod();
            Nt.Data.PaymentType paymentType;
            Data.ntCachedPaymentTypes.TryGetValue(data.MediumID, out paymentType);

            if (data.ManualData != null)
            {
                foreach (var line in data.ManualData.Lines)
                {
                    switch (line.Key) {
                        case "ROOM":
                            var room = Task.Run(async () => await Nt.Database.DB.Api.Hotel.GetRoom(session, paymentType.PartnerId, line.Value)).Result;
                            if (room == null)
                                throw new Exception("Zimmer " + line.Value + " nicht gefunden");
                            //set up dialog
                            preAuthResult.Dialog = new Models.OsDialog();
                            preAuthResult.Dialog.Header = "Zimmerabrechnung";
                            preAuthResult.Dialog.Message = "Zimmer für " + room.Name;
                            preAuthResult.Dialog.Type = Models.DialogType.YesNoEnum;
                            paymentMethod.RoomNumber = room.Id;
                            paymentMethod.RoomBookingNumber = room.BookingNumber;
                            break;
                    }
                }
            }

            if (data.ReqAmount != null)
            {
                paymentMethod.Amount = decimal.Divide((decimal)data.ReqAmount, 100m);
                preAuthMedium.AuthAmount = data.ReqAmount;
            }
            else
            {
                paymentMethod.Amount = 0;
                preAuthMedium.AuthAmount = 0;
            }

            if (data.ReqTip != null)
            {
                paymentMethod.Tip = decimal.Divide((decimal)data.ReqTip, 100m);
                preAuthMedium.AuthTip = data.ReqTip;
            }
            else
            {
                paymentMethod.Tip = 0;
                preAuthMedium.AuthTip = 0;
            }

            preAuthMedium.AuthCode = Guid.NewGuid().ToString();
            preAuthResult.AuthMedia = new List<Models.PreAuthMedium>();
            //
            paymentMethod.PaymentTypeId = paymentType.Id;
            paymentMethod.AssignmentTypeId = "N";
            paymentMethod.PartnerId = paymentType.PartnerId;
            paymentMethod.Program = paymentType.Program;
            //save Medium in Result and global
            preAuthResult.AuthMedia.Add(preAuthMedium);
            AuthData.Add(preAuthMedium.AuthCode, paymentMethod);
            Nt.Logging.Log.Server.Debug("AuthData contains " + AuthData.Count + " values after adding guid " + preAuthMedium.AuthCode);

            return preAuthResult;
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

        private static List<PaymentMethod> GetNtPaymentMethods(List<Models.Payment> osPayments, int? tip)
        {
            var ntPaymentMethods = new List<Nt.Data.PaymentMethod>();

            foreach (var osPayment in osPayments)
            {
                var ntPaymentMethod = GetPaymentMethod(osPayment.PaymentMediumId);

                // preauthorized paymentType
                if (osPayment.AuthCode != null)
                {
                    Nt.Data.PaymentMethod authorizedPaymentMethod;
                    if (AuthData.TryGetValue(osPayment.AuthCode, out authorizedPaymentMethod))
                    {
                        ntPaymentMethod = authorizedPaymentMethod;
                        AuthData.Remove(osPayment.AuthCode);
                        Nt.Logging.Log.Server.Debug("AuthData contains " + AuthData.Count + " values after removing guid " + osPayment.AuthCode);
                    }
                }

                //tip
                if (tip > 0)
                {
                    ntPaymentMethod.Tip = decimal.Divide((decimal)tip, 100.0m);
                    tip = 0;
                }

                ntPaymentMethod.Amount = decimal.Divide((decimal)osPayment.AmountPaid, 100.0m) - ntPaymentMethod.Tip;
                ntPaymentMethods.Add(ntPaymentMethod);
            }

            return ntPaymentMethods;
        }
    }
}