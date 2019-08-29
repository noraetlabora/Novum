using System;
using System.Collections.Generic;
using Nt.Database.Api;

//[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

namespace Nt.Database.Api.InterSystems
{
    internal class Fiscal : IDbFiscal
    {

        //private static readonly log4net.ILog fiscalLog = log4net.LogManager.GetLogger("FISCAL - API");

        /// <summary>
        /// 
        /// </summary>
        public Fiscal()
        {
            //fiscalLog.Error("XX");
            //var repository = log4net.LogManager.GetRepository();
            //
            //log.Error("hello");

            //Nov.NT.POS.Fiscal.Logging.APILogger.Error("hallo");
            //Nov.NT.POS.Fiscal.Logging.FiscalLogger.Warn("you");
            //Nov.NT.POS.Fiscal.Logging.ProviderLogger.Fatal("there");
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
            {
                return null;
            }

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
    }
}
