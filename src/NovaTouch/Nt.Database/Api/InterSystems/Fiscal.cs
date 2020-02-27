using Nt.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// <param name="session"></param>
        /// <returns>Returns the mode (fiscalization = 0, no fiscalization = 1,2) as string.</returns>
        public Task<string> GetMode(Data.Session session)
        {
            return Intersystems.Instance.CallClassMethod("cmNT.Fiskal", "GetFiskalModus", session.ClientId, session.PosId);
        }

        /// <summary>
        /// 1 = old modul version (VB6)
        /// 2 = new modul version (.NET)
        /// </summary>
        /// <param name="session"></param>
        /// <returns>Returns the modul version (VB6 = 1, .NET = 2).</returns>
        public Task<string> GetModulVersion(Data.Session session)
        {
            return Intersystems.Instance.CallClassMethod("cmNT.Fiskal", "GetFiskalModulV", session.ClientId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public async Task<string> GetServiceType(Data.Session session)
        {
            return await Intersystems.Instance.CallClassMethod("cmNT.Fiskal", "GetProvider", session.ClientId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public async Task<string> GetConfiguration(Data.Session session)
        {
            return await Intersystems.Instance.CallClassMethod("cmNT.Fiskal", "GetConfig", session.ClientId, session.PosId, string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public Task<string> GetClient(Data.Session session)
        {
            return Intersystems.Instance.CallClassMethod("cmNT.Fiskal", "GetMandant", session.ClientId);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public async Task<string> GetUser(Data.Session session)
        {
            return await Intersystems.Instance.CallClassMethod("cmNT.Fiskal", "GetBediener", session.ClientId, session.PosId, session.WaiterId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public async Task<object> GetProvider(Data.Session session)
        {
            var fiscalMode = this.GetMode(session);
            if (!fiscalMode.Equals("0"))
                return null;

            var fiscalConfiguration = await GetConfiguration(session);
            var fiscalServiceType = await GetServiceType(session);

            Nov.NT.POS.Fiscal.IFiscalProvider fiscalProvider;
            try
            {
                fiscalProvider = Nov.NT.POS.Fiscal.ProviderFactory.GetFiscalProvider(fiscalServiceType);
                fiscalProvider.ApplyConfiguration(fiscalConfiguration);
                fiscalProvider.Open(session.SerialNumber);
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
        public async Task<string> CheckSystem(Data.Session session)
        {
            if (session.FiscalProvider == null)
                return string.Empty;

            var fiscalProvider = (Nov.NT.POS.Fiscal.IFiscalProvider)session.FiscalProvider;
            var fiscalClientString = await GetClient(session);
            var fiscalClient = new Nov.NT.POS.Data.DTO.FiscalParameterMandantDTO(fiscalClientString);
            var fiscalUserString = await GetUser(session);
            var fiscalUser = new Nov.NT.POS.Data.DTO.FiscalParameterBedienerDTO(fiscalUserString);

            try
            {
                var fiscalResult = fiscalProvider.CheckFiscalSystem(fiscalClient, fiscalUser);
                if (fiscalResult.IsOK)
                    return string.Empty;
                else
                    return fiscalResult.StatusHeader + " " + fiscalResult.StatusText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="orders"></param>
        /// <param name="paymentMethods"></param>
        /// <param name="paymentInformation"></param>
        public async Task<object> SendTransaction(Data.Session session, List<Nt.Data.Order> orders, List<Nt.Data.PaymentMethod> paymentMethods, Nt.Data.PaymentInformation paymentInformation)
        {
            if (session.FiscalProvider == null)
                return null;

            var ordersDataString = Order.GetOrderDataString(orders);
            var paymentMethodsDataString = Payment.GetPaymentMethodDataString(paymentMethods);
            var paymentBillDataString = Payment.GetPaymentBillDataString(paymentInformation);
            var paymentOptionDataString = Payment.GetPaymentOptionDataString(paymentInformation);

            Nov.NT.POS.Fiscal.FiscalResult fiscalResult = null;
            var fiscalProvider = (Nov.NT.POS.Fiscal.IFiscalProvider)session.FiscalProvider;
            var fiscalClientString = await GetClient(session);
            var fiscalUserString = await GetUser(session);
            var fiscalData = await GetData(session, ordersDataString, paymentMethodsDataString, paymentBillDataString);
            if (fiscalData.StartsWith("FM"))
            {
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
                await RollbackData(session, ordersDataString, paymentMethodsDataString, paymentMethodsDataString, paymentBillDataString, "");
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
        public async Task RollbackTransaction(Data.Session session, string reason)
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
        public Task CommitTransaction(Data.Session session)
        {
            if (session.FiscalProvider == null)
                return Task.CompletedTask;

            var fiscalProvider = (Nov.NT.POS.Fiscal.IFiscalProvider)session.FiscalProvider;
            fiscalProvider.TransactionCommit();
            return Task.CompletedTask;
        }

        public Task<string> RollbackData(Session session, string ordersDataString, string paymentMethodsDataString, string paymentBillDataString)
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
        private Task<string> GetData(Session session, string ordersDataString, string paymentMethodsDataString, string paymentBillDataString)
        {
            return Intersystems.Instance.CallClassMethod("cmNT.AbrOman2", "GetFiskalDaten", session.ClientId, session.PosId, session.WaiterId, session.CurrentTable.Id, ordersDataString, session.PriceLevel, paymentBillDataString, paymentMethodsDataString, "", "1");
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
        private Task<string> RollbackData(Nt.Data.Session session, string tableId, string ordersStringData, string paymentMethodsStringData, string paymentBillStringData, string paymentOptionStringData)
        {
            return Intersystems.Instance.CallClassMethod("cmNT.AbrOman2", "RollbackFiskalDaten", session.ClientId, session.PosId, session.WaiterId, tableId, ordersStringData, session.PriceLevel, paymentBillStringData, paymentMethodsStringData, paymentOptionStringData);
        }

        #endregion
    }
}
