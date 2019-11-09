using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Nt.Data;

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
        public string CheckSystem(Data.Session session)
        {
            if (session.FiscalProvider == null)
                return string.Empty;

            var fiscalProvider = (Nov.NT.POS.Fiscal.IFiscalProvider)session.FiscalProvider;
            var fiscalClientString = DB.Api.Fiscal.GetClient(session.ClientId);
            var fiscalClient = new Nov.NT.POS.Data.DTO.FiscalParameterMandantDTO(fiscalClientString);
            var fiscalUserString = DB.Api.Fiscal.GetUser(session.ClientId, session.PosId, session.WaiterId);
            var fiscalUser = new Nov.NT.POS.Data.DTO.FiscalParameterBedienerDTO(fiscalUserString);
            
            try
            {
                var fiscalResult = fiscalProvider.CheckFiscalSystem(fiscalClient, fiscalUser);
                if (fiscalResult.IsOK)
                    return string.Empty;
                else
                    return fiscalResult.StatusHeader + " " + fiscalResult.StatusText;
            }
            catch(Exception ex)
            {
                throw ex;
            }
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
            var fiscalData = GetData(session, ordersDataString, paymentMethodsDataString, paymentBillDataString);
            if (fiscalData.StartsWith("FM")) {
                var fiscalDataString = new DataString(fiscalData);
                var fiscalDataList = fiscalDataString.SplitByChar96();
                throw new Exception(fiscalDataList[1]);
            }
            Nov.NT.POS.Data.DTO.AbrechnungDTOHelper.ApplyAbrechnungRabattDaten(ref paymentBillDataString, ref ordersDataString, ref paymentMethodsDataString, fiscalData);
            var fiscalReceipt = new Nov.NT.POS.Data.DTO.FiscalBelegDTO(fiscalClientString, fiscalUserString, paymentBillDataString, ordersDataString, paymentMethodsDataString);

            try
            {
                fiscalResult = fiscalProvider.SendFiscalBeleg(fiscalReceipt);
            }
            catch (Exception ex)
            {
                fiscalProvider.TransactionRollback("Fiscal Transaction Rollback because of Exception " + ex.Message);
                throw ex;
            }

            if (!fiscalResult.IsOK)
            {
                RollbackData(session, ordersDataString, paymentMethodsDataString, paymentMethodsDataString, paymentBillDataString, "");
                var message = string.Format("{0} {1}", fiscalResult.StatusHeader, fiscalResult.StatusText);
                fiscalProvider.TransactionRollback(message);
                throw new Exception(message);
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

        public string RollbackData(Session session, string ordersDataString, string paymentMethodsDataString, string paymentBillDataString)
        {
            throw new NotImplementedException();
        }

        #region private

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="ordersDataString"></param>
        /// <param name="paymentMethodsDataString"></param>
        /// <param name="paymentBillDataString"></param>
        /// <returns></returns>
        private string GetData(Session session, string ordersDataString, string paymentMethodsDataString, string paymentBillDataString)
        {
            return Interaction.CallClassMethod("cmNT.AbrOman2", "GetFiskalDaten", session.ClientId, session.PosId, session.WaiterId, session.CurrentTable.Id, ordersDataString, session.PriceLevel, paymentBillDataString, paymentMethodsDataString, "", "1");
        }

        private string GetReceiptNumber(string data)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <param name="ordersStringData"></param>
        /// <param name="paymentMethodsStringData"></param>
        /// <param name="paymentBillStringData"></param>
        /// <param name="paymentOptionStringData"></param>
        /// <returns></returns>
        private string RollbackData(Nt.Data.Session session, string tableId, string ordersStringData, string paymentMethodsStringData, string paymentBillStringData, string paymentOptionStringData)
        {
            return Interaction.CallClassMethod("cmNT.AbrOman2", "RollbackFiskalDaten", session.ClientId, session.PosId, session.WaiterId, tableId, ordersStringData, session.PriceLevel, paymentBillStringData, paymentMethodsStringData, paymentOptionStringData);
        }

        #endregion
    }
}
