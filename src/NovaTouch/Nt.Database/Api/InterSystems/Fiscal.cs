using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Nt.Database.Api.InterSystems
{
    internal class Fiscal : IDbFiscal
    {
        /// <summary>
        /// 
        /// </summary>
        public Fiscal()
        {
        }

        /// <summary>
        /// 0 = fiscalization
        /// 1 = no fiscalization - just payment methods where we don't need fiscalization
        /// 2 = no fiscalization due to the law
        /// </summary>
        /// <param name="clientId">The current client id ("1001").</param>
        /// <param name="posId">The current pos id ("RK2").</param>
        /// <returns>Returns the mode (fiscalization = 0, no fiscalization = 1,2) as string.</returns>
        public string GetMode(string clientId, string posId)
        {
            return Interaction.CallClassMethod("cmNT.Fiskal", "GetFiskalModus", clientId, posId);
        }

        /// <summary>
        /// 1 = old modul version (VB6)
        /// 2 = new modul version (.NET)
        /// </summary>
        /// <param name="clientId">The current client id ("1001").</param>
        /// <returns>Returns the modul version (VB6 = 1, .NET = 2).</returns>
        public string GetModulVersion(string clientId)
        {
            return Interaction.CallClassMethod("cmNT.Fiskal", "GetFiskalModulV", clientId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId">The current client id ("1001").</param>
        /// <returns>Returns the service type </returns>
        public string GetServiceType(string clientId)
        {
            return Interaction.CallClassMethod("cmNT.Fiskal", "GetProvider", clientId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId">The current client id ("1001").</param>
        /// <param name="posId">The current pos id ("RK2").</param>
        /// <returns></returns>
        public string GetConfiguration(string clientId, string posId)
        {
            return Interaction.CallClassMethod("cmNT.Fiskal", "GetConfig", clientId, posId, string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session">Session data.</param>
        /// <param name="tableId"></param>
        /// <param name="orders"></param>
        /// <param name="paymentMethods"></param>
        /// <param name="paymentInformation"></param>
        /// <returns></returns>
        public string GetData(Nt.Data.Session session, string tableId, List<Nt.Data.Order> orders, List<Nt.Data.PaymentMethod> paymentMethods, Nt.Data.PaymentInformation paymentInformation)
        {
            var ordersStringData = Order.GetOrderDataString(orders);
            var paymentMethodsStringData = Payment.GetPaymentMethodDataString(paymentMethods);
            var paymentBillStringData = Payment.GetPaymentBillDataString(paymentInformation);
            var paymentOptionStringData = Payment.GetPaymentOptionDataString(paymentInformation);

            var dbString = Interaction.CallClassMethod("cmNT.AbrOman2", "GetFiskalDaten", session.ClientId, session.PosId, session.WaiterId, tableId, ordersStringData, session.PriceLevel, paymentBillStringData, paymentMethodsStringData, paymentOptionStringData, "1");
            return dbString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId">The current client id ("1001").</param>
        /// <returns></returns>
        public string GetClient(string clientId)
        {
            return Interaction.CallClassMethod("cmNT.Fiskal", "GetMandant", clientId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId">The current client id ("1001").</param>
        /// <param name="posId">The current pos id ("RK2").</param>
        /// <param name="waiterId"></param>
        /// <returns></returns>
        public string GetUser(string clientId, string posId, string waiterId)
        {
            return Interaction.CallClassMethod("cmNT.Fiskal", "GetBediener", clientId, posId, waiterId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId">The current client id ("1001").</param>
        /// <param name="posId">The current pos id ("RK2").</param>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        public object GetProvider(string clientId, string posId, string serialNumber)
        {
            var fiscalMode = this.GetMode(clientId, posId);
            if (!fiscalMode.Equals("0"))
                return null;

            var fiscalConfiguration = this.GetConfiguration(clientId, posId);
            var fiscalServiceType = this.GetServiceType(clientId);

            Nov.NT.POS.Fiscal.IFiscalProvider fiscalProvider;
            try
            {
                fiscalProvider = Nov.NT.POS.Fiscal.ProviderFactory.GetFiscalProvider(fiscalServiceType);
                fiscalProvider.ApplyConfiguration(fiscalConfiguration);
                fiscalProvider.Open(serialNumber);
            }
            catch (Exception ex)
            {
                Logging.Log.Server.Error(ex, "Error GetFiscalProvider");
                throw ex;
            }
            return fiscalProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="ordersDataString"></param>
        /// <param name="paymentMethodsDataString"></param>
        /// <param name="paymentBillDataString"></param>
        public object SendTransaction(Data.Session session, string ordersDataString, string paymentMethodsDataString, string paymentBillDataString)
        {
            if (session.FiscalProvider == null)
                return null;

            Nov.NT.POS.Fiscal.FiscalResult fiscalResult = null;
            var fiscalProvider = (Nov.NT.POS.Fiscal.IFiscalProvider)session.FiscalProvider;
            var fiscalClientString = DB.Api.Fiscal.GetClient(session.ClientId);
            var fiscalUserString = DB.Api.Fiscal.GetUser(session.ClientId, session.PosId, session.WaiterId);
            var fiscalReceipt = new Nov.NT.POS.Data.DTO.FiscalBelegDTO(fiscalClientString, fiscalUserString, paymentBillDataString, ordersDataString, paymentMethodsDataString);

            try
            {
                fiscalResult = fiscalProvider.SendFiscalBeleg(fiscalReceipt);
            }
            catch (Exception ex)
            {
                fiscalProvider.TransactionRollback("Fiscal Transaction Rollback because of Exception " + ex.Message);
                throw new Exception(ex.Message);
            }

            if (!fiscalResult.IsOK)
            {
                fiscalProvider.TransactionRollback("Fiscal Transaction Rollback because of result is not OK " + fiscalResult.UserMessage);
                throw new Exception(fiscalResult.UserMessage);
            }

            return fiscalResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="reason"></param>
        public void RollbackTransaction(Data.Session session, string reason)
        {
            if (session.FiscalProvider == null)
                return;

            var fiscalProvider = (Nov.NT.POS.Fiscal.IFiscalProvider)session.FiscalProvider;
            fiscalProvider.TransactionRollback(reason);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public void CommitTransaction(Data.Session session)
        {
            if (session.FiscalProvider == null)
                return;

            var fiscalProvider = (Nov.NT.POS.Fiscal.IFiscalProvider)session.FiscalProvider;
            fiscalProvider.TransactionCommit();
        }
    }
}
