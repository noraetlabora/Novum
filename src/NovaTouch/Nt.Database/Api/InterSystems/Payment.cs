using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Database.Api.Intersystems
{
    /// <summary>
    /// 
    /// </summary>
    internal class Payment : IDbPayment
    {
        public Payment() { }

        #region public methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, Nt.Data.PaymentType>> GetPaymentTypes()
        {
            var paymentTypes = new Dictionary<string, Nt.Data.PaymentType>();
            var sql = new StringBuilder();
            sql.Append(" SELECT IKA, bez, prg, druanz, unterschrift, Copa ");
            sql.Append(" FROM NT.Zahlart ");
            sql.Append(" WHERE FA = ").Append(Api.ClientId);
            sql.Append(" AND passiv > ").Append(Intersystems.SqlToday);
            var dataTable = await Intersystems.GetDataTable(sql.ToString()).ConfigureAwait(false);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var paymentType = new Nt.Data.PaymentType();
                paymentType.Id = DataObject.GetString(dataRow, "IKA");
                paymentType.Name = DataObject.GetString(dataRow, "bez");
                paymentType.Program = DataObject.GetString(dataRow, "prg");
                paymentType.ReceiptCount = DataObject.GetUInt(dataRow, "druanz");
                paymentType.PartnerId = DataObject.GetString(dataRow, "Copa");
                var signature = DataObject.GetString(dataRow, "unterschrift");

                if (signature.Equals("0"))
                    paymentType.Signature = true;
                else
                    paymentType.Signature = false;

                if (!paymentTypes.ContainsKey(paymentType.Id))
                    paymentTypes.Add(paymentType.Id, paymentType);
            }

            return paymentTypes;
        }

        public async Task<Nt.Data.PaymentResult> Pay(Nt.Data.Session session, string tableId, List<Nt.Data.Order> orders, List<Nt.Data.PaymentMethod> paymentMethods, Nt.Data.PaymentInformation paymentInformation)
        {
            var ordersDataString = Order.GetOrderDataString(orders);
            var paymentMethodsDataString = Payment.GetPaymentMethodDataString(paymentMethods);
            var paymentBillDataString = Payment.GetPaymentBillDataString(paymentInformation);
            var paymentOptionDataString = Payment.GetPaymentOptionDataString(paymentInformation);

            //send the Fiscal Transaction if there is a provider
            var fiscalResult = (Nov.NT.POS.Fiscal.FiscalResult)await DB.Api.Fiscal.SendTransaction(session, orders, paymentMethods, paymentInformation).ConfigureAwait(false);
            var fiscalResultString = string.Empty;
            if (fiscalResult != null)
                fiscalResultString = fiscalResult.ToDtoString();

            //actual payment in database
            var args = new object[15] { session.ClientId, session.PosId, session.WaiterId, tableId, session.SerialNumber, ordersDataString, paymentBillDataString, paymentMethodsDataString, paymentOptionDataString, "", "", "", "", "", fiscalResultString };
            var dbString = await Intersystems.CallClassMethod("cmNT.AbrOman2", "DoAbrechnung", args).ConfigureAwait(false);

            if (dbString.StartsWith("FM"))
            {
                await DB.Api.Fiscal.RollbackTransaction(session, dbString).ConfigureAwait(false);
                throw new Exception(dbString);
            }

            var result = new Nt.Data.PaymentResult();
            var paymentString = new DataString(dbString);
            var paymentArray = paymentString.SplitByDoublePipes();
            var paymentList = new DataList(paymentArray);

            result.BillId = paymentList.GetString(3);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, Nt.Data.AssignmentType>> GetAssignmentTypes()
        {
            var assignmentTypes = new Dictionary<string, Nt.Data.AssignmentType>();
            var sql = new StringBuilder();
            sql.Append(" SELECT VA, bez, unterschrift ");
            sql.Append(" FROM WW.VA ");
            sql.Append(" WHERE FA = ").Append(Api.ClientId);
            sql.Append(" AND passiv > ").Append(Intersystems.SqlToday);
            var dataTable = await Intersystems.GetDataTable(sql.ToString()).ConfigureAwait(false);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var assignmentType = new Nt.Data.AssignmentType();
                assignmentType.Id = DataObject.GetString(dataRow, "VA");
                assignmentType.Name = DataObject.GetString(dataRow, "bez");
                var signature = DataObject.GetString(dataRow, "unterschrift");

                if (signature.Equals("0"))
                    assignmentType.Signature = true;
                else
                    assignmentType.Signature = false;

                if (!assignmentTypes.ContainsKey(assignmentType.Id))
                    assignmentTypes.Add(assignmentType.Id, assignmentType);
            }

            return assignmentTypes;
        }

        #endregion 

        #region internal methods

        internal static string GetPaymentMethodDataString(List<Nt.Data.PaymentMethod> paymentMethods)
        {
            var dataString = new StringBuilder();
            foreach (var paymentMethod in paymentMethods)
            {
                if (dataString.Length > 0)
                    dataString.Append(DataString.Char96);
                dataString.Append(GetPaymentMethodDataString(paymentMethod));
            }
            return dataString.ToString();
        }

        internal static string GetPaymentMethodDataString(Nt.Data.PaymentMethod paymentMethod)
        {
            var dataString = new StringBuilder();

            dataString.Append(paymentMethod.PaymentTypeId).Append(DataString.DoublePipes);
            dataString.Append(paymentMethod.AssignmentTypeId).Append(DataString.DoublePipes);
            dataString.Append(paymentMethod.Name).Append(DataString.DoublePipes);
            dataString.Append(paymentMethod.Program).Append(DataString.DoublePipes);
            dataString.Append(paymentMethod.Amount).Append(DataString.DoublePipes);
            dataString.Append(paymentMethod.Tip).Append(DataString.DoublePipes);
            dataString.Append(paymentMethod.Comment).Append(DataString.DoublePipes);
            dataString.Append("").Append(DataString.DoublePipes); //Adresse
            dataString.Append(paymentMethod.PartnerId).Append(DataString.DoublePipes); //ZimmerCOPA
            dataString.Append(paymentMethod.RoomBookingNumber).Append(DataString.DoublePipes); //ZimmerBuchnr
            dataString.Append(paymentMethod.RoomNumber).Append(DataString.DoublePipes); //ZimmerNummer
            dataString.Append("").Append(DataString.DoublePipes); //Ids
            dataString.Append("").Append(DataString.DoublePipes); //Beschreibung
            dataString.Append("").Append(DataString.DoublePipes); //SerNr
            dataString.Append("").Append(DataString.DoublePipes); //SamreKdnr
            dataString.Append("").Append(DataString.DoublePipes); //UnterschriftModus
            dataString.Append("").Append(DataString.DoublePipes); //Kostenstelle
            dataString.Append("").Append(DataString.DoublePipes); //KreditkartenTransaktion
            dataString.Append("").Append(DataString.DoublePipes); //RfidTID
            dataString.Append("").Append(DataString.DoublePipes); //RfidDaten

            return dataString.ToString();
        }

        internal static string GetPaymentBillDataString(Nt.Data.PaymentInformation paymentInformation)
        {
            var dataString = new StringBuilder();

            dataString.Append("").Append(DataString.DoublePipes);//VKO
            dataString.Append("").Append(DataString.DoublePipes);//GesamtBetragBrutto
            dataString.Append(paymentInformation.DiscountAmount).Append(DataString.DoublePipes);
            dataString.Append(paymentInformation.DiscountId).Append(DataString.DoublePipes);
            dataString.Append("").Append(DataString.DoublePipes);//Adresse
            dataString.Append(paymentInformation.Guests).Append(DataString.DoublePipes);
            dataString.Append("").Append(DataString.DoublePipes);//OrderNr
            dataString.Append("").Append(DataString.DoublePipes);//OptPrinter
            dataString.Append("").Append(DataString.DoublePipes);//OptBewirtungsbeleg
            dataString.Append("").Append(DataString.DoublePipes);//OptAusserHaus
            dataString.Append("").Append(DataString.DoublePipes);//UnterschriftModus
            dataString.Append("").Append(DataString.DoublePipes);//UnterschriftData
            dataString.Append("").Append(DataString.DoublePipes);//AdressNr
            dataString.Append("").Append(DataString.DoublePipes);//OptKeinBelegDruck
            dataString.Append("").Append(DataString.DoublePipes);//?
            dataString.Append("").Append(DataString.DoublePipes);//FreifeldDaten
            dataString.Append(paymentInformation.BenefitAmount).Append(DataString.DoublePipes);
            dataString.Append(paymentInformation.BenefitId).Append(DataString.DoublePipes);
            dataString.Append(paymentInformation.PriceLevel).Append(DataString.DoublePipes);
            dataString.Append("").Append(DataString.DoublePipes);//TimeStamp
            dataString.Append("").Append(DataString.DoublePipes);//BelegNr
            dataString.Append("").Append(DataString.DoublePipes);//Kundenkarte
            dataString.Append("").Append(DataString.DoublePipes);//KundenkartenDaten
            dataString.Append("").Append(DataString.DoublePipes);//OptPrinterDev
            dataString.Append("").Append(DataString.DoublePipes);//OptPrinterName
            dataString.Append("").Append(DataString.DoublePipes);//FiskalBelegType

            return dataString.ToString();
        }

        internal static string GetPaymentOptionDataString(Nt.Data.PaymentInformation paymentInformation)
        {
            var dataString = new StringBuilder();

            dataString.Append("").Append(DataString.DoublePipes);//Bewirtungsbeleg
            dataString.Append("").Append(DataString.DoublePipes);//ZweiteUstGruppe
            dataString.Append("").Append(DataString.DoublePipes);//ReserveDrucker
            dataString.Append("").Append(DataString.DoublePipes);//obsoletKeinBelegDruck
            dataString.Append(paymentInformation.PrinterId).Append(DataString.DoublePipes);//BelegDrucker

            return dataString.ToString();
        }

        #endregion
    }
}