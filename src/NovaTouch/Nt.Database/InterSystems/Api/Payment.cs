using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Nt.Database.Api;

namespace Nt.Database.InterSystems.Api
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
        public Dictionary<string, Nt.Data.PaymentType> GetPaymentTypes()
        {
            var paymentTypes = new Dictionary<string, Nt.Data.PaymentType>();
            var sql = new StringBuilder();
            sql.Append(" SELECT IKA, bez, prg, druanz, unterschrift ");
            sql.Append(" FROM NT.Zahlart ");
            sql.Append(" WHERE FA = ").Append(InterSystemsApi.ClientId);
            sql.Append(" AND passiv > ").Append(Interaction.SqlToday);
            var dataTable = Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var paymentType = new Nt.Data.PaymentType();
                paymentType.Id = DataObject.GetString(dataRow, "IKA");
                paymentType.Name = DataObject.GetString(dataRow, "bez");
                paymentType.Program = DataObject.GetString(dataRow, "prg");
                paymentType.ReceiptCount = DataObject.GetUInt(dataRow, "druanz");
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

        public Nt.Data.PaymentResult Pay(Nt.Data.Session session, string tableId, List<Nt.Data.Order> orders, List<Nt.Data.PaymentMethod> paymentMethods, Nt.Data.PaymentInformation paymentInformation) {
            var ordersStringData = Order.GetOrderDataString(orders);
            var paymentMethodsStringData = Payment.GetPaymentMethodDataString(paymentMethods);
            var paymentBillStringData = Payment.GetPaymentBillDataString(paymentInformation);
            var paymentOptionStringData = Payment.GetPaymentOptionDataString(paymentInformation);

            var dbString = Interaction.CallClassMethod("cmNT.AbrOman2", "DoAbrechnung", session.ClientId, session.PosId, session.WaiterId, tableId, session.SerialNumber, ordersStringData, paymentBillStringData, paymentMethodsStringData, paymentOptionStringData, "", "", "", "", "", "");

            if (dbString.StartsWith("FM"))
                throw new Exception(dbString);

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
        public Dictionary<string, Nt.Data.AssignmentType> GetAssignmentTypes()
        {
            var assignmentTypes = new Dictionary<string, Nt.Data.AssignmentType>();
            var sql = new StringBuilder();
            sql.Append(" SELECT VA, bez, unterschrift ");
            sql.Append(" FROM WW.VA ");
            sql.Append(" WHERE FA = ").Append(InterSystemsApi.ClientId);
            sql.Append(" AND passiv > ").Append(Interaction.SqlToday);
            var dataTable = Interaction.GetDataTable(sql.ToString());

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
                    dataString.Append(DataString.CRLF);
                dataString.Append(GetPaymentMethodDataString(paymentMethod));
            }
            return dataString.ToString();
        }

        internal static string GetPaymentMethodDataString(Nt.Data.PaymentMethod paymentMethod)
        {
            var dataString = new StringBuilder();
            //
            dataString.Append(paymentMethod.PaymentTypeId).Append(DataString.DoublePipes);
            dataString.Append(paymentMethod.AssignmentTypeId).Append(DataString.DoublePipes);
            dataString.Append("").Append(DataString.DoublePipes); //ZahlartText
            dataString.Append(paymentMethod.Program).Append(DataString.DoublePipes);
            dataString.Append(paymentMethod.Amount).Append(DataString.DoublePipes);
            dataString.Append(paymentMethod.Tip).Append(DataString.DoublePipes);
            dataString.Append(paymentMethod.Comment).Append(DataString.DoublePipes);
            dataString.Append("").Append(DataString.DoublePipes); //Adresse
            dataString.Append("").Append(DataString.DoublePipes); //ZimmerCOPA
            dataString.Append("").Append(DataString.DoublePipes); //ZimmerBuchnr
            dataString.Append("").Append(DataString.DoublePipes); //Ids
            dataString.Append("").Append(DataString.DoublePipes); //Beschreibung
            dataString.Append("").Append(DataString.DoublePipes); //SerNr
            dataString.Append("").Append(DataString.DoublePipes); //SamreKdnr
            dataString.Append("").Append(DataString.DoublePipes); //UnterschriftModus
            dataString.Append("").Append(DataString.DoublePipes); //Kostenstelle
            dataString.Append("").Append(DataString.DoublePipes); //KreditkartenTransaktion
            dataString.Append("").Append(DataString.DoublePipes); //RfidTID
            dataString.Append("").Append(DataString.DoublePipes); //RfidDaten
            //
            return dataString.ToString();
        }

        internal static string GetPaymentBillDataString(Nt.Data.PaymentInformation paymentInformation)
        {
            var dataString = new StringBuilder();
            //
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
            //
            return dataString.ToString();
        }

        internal static string GetPaymentOptionDataString(Nt.Data.PaymentInformation paymentInformation)
        {
            var dataString = new StringBuilder();
            //
            dataString.Append("").Append(DataString.DoublePipes);//Bewritungsbeleg
            dataString.Append("").Append(DataString.DoublePipes);//ZweiteUstGruppe
            dataString.Append("").Append(DataString.DoublePipes);//ReserveDrucker
            dataString.Append("").Append(DataString.DoublePipes);//obsoletKeinBelegDruck
            dataString.Append("").Append(DataString.DoublePipes);//BelegDrucker
            //
            return dataString.ToString();
        }

        #endregion
    }
}