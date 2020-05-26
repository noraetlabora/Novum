using Nt.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nt.Database.Api.Intersystems
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
            var args = new object[2] { session.ClientId, session.PosId };
            return Intersystems.CallClassMethod("cmNT.Fiskal", "GetFiskalModus", args);
        }

        /// <summary>
        /// 1 = old modul version (VB6)
        /// 2 = new modul version (.NET)
        /// </summary>
        /// <param name="session"></param>
        /// <returns>Returns the modul version (VB6 = 1, .NET = 2).</returns>
        public Task<string> GetModulVersion(Data.Session session)
        {
            var args = new object[1] { session.ClientId };
            return Intersystems.CallClassMethod("cmNT.Fiskal", "GetFiskalModulV", args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public Task<string> GetServiceType(Data.Session session)
        {
            var args = new object[1] { session.ClientId };
            return Intersystems.CallClassMethod("cmNT.Fiskal", "GetProvider", args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public Task<string> GetConfiguration(Data.Session session)
        {
            var args = new object[3] { session.ClientId, session.PosId, string.Empty };
            return Intersystems.CallClassMethod("cmNT.Fiskal", "GetConfig", args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public Task<string> GetClient(Data.Session session)
        {
            var args = new object[1] { session.ClientId };
            return Intersystems.CallClassMethod("cmNT.Fiskal", "GetMandant", args);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public Task<string> GetUser(Data.Session session)
        {
            var args = new object[3] { session.ClientId, session.PosId, session.WaiterId };
            return Intersystems.CallClassMethod("cmNT.Fiskal", "GetBediener", args);
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

            var fiscalConfiguration = await GetConfiguration(session).ConfigureAwait(false);
            var fiscalServiceType = await GetServiceType(session).ConfigureAwait(false);

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
            var fiscalClientString = await GetClient(session).ConfigureAwait(false);
            var fiscalClient = new Nov.NT.POS.Data.DTO.FiscalParameterMandantDTO(fiscalClientString);
            var fiscalUserString = await GetUser(session).ConfigureAwait(false);
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
            var fiscalClientString = await GetClient(session).ConfigureAwait(false);
            var fiscalUserString = await GetUser(session).ConfigureAwait(false);
            var fiscalData = await GetData(session, ordersDataString, paymentMethodsDataString, paymentBillDataString).ConfigureAwait(false);
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
                await RollbackData(session, ordersDataString, paymentMethodsDataString, paymentMethodsDataString, paymentBillDataString, "").ConfigureAwait(false);
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
        public Task RollbackTransaction(Data.Session session, string reason)
        {
            if (session.FiscalProvider == null)
                return Task.CompletedTask;

            var fiscalProvider = (Nov.NT.POS.Fiscal.IFiscalProvider)session.FiscalProvider;
            fiscalProvider.TransactionRollback(reason);
            return Task.CompletedTask;
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
            var args = new object[10] { session.ClientId, session.PosId, session.WaiterId, session.CurrentTable.Id, ordersDataString, session.PriceLevel, paymentBillDataString, paymentMethodsDataString, "", "1" };
            return Intersystems.CallClassMethod("cmNT.AbrOman2", "GetFiskalDaten", args);
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
            var args = new object[9] { session.ClientId, session.PosId, session.WaiterId, tableId, ordersStringData, session.PriceLevel, paymentBillStringData, paymentMethodsStringData, paymentOptionStringData };
            return Intersystems.CallClassMethod("cmNT.AbrOman2", "RollbackFiskalDaten", args);
        }

        #endregion
    }
}
