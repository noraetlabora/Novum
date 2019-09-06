using System.Collections.Generic;

namespace Nt.Database.Api
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbFiscal
    {
        /// <summary>
        /// 0 = fiscalization
        /// 1 = no fiscalization - just payment methods where we don't need fiscalization
        /// 2 = no fiscalization due to the law
        /// </summary>
        /// <param name="clientId">The current client id ("1001").</param>
        /// <param name="posId">The current pos id ("RK2").</param>
        /// <returns></returns>
        string GetMode(string clientId, string posId);

        /// <summary>
        /// 1 = old modul version (VB6)
        /// 2 = new modul version (.NET)
        /// </summary>
        /// <param name="clientId">The current client id ("1001").</param>
        /// <returns></returns>
        string GetModulVersion(string clientId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId">The current client id ("1001").</param>
        /// <returns></returns>
        string GetServiceType(string clientId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId">The current client id ("1001").</param>
        /// <param name="posId">The current pos id ("RK2").</param>
        /// <returns></returns>
        string GetConfiguration(string clientId, string posId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session">Session data.</param>
        /// <param name="tableId"></param>
        /// <param name="orders"></param>
        /// <param name="paymentMethods"></param>
        /// <param name="paymentInformation"></param>
        /// <returns></returns>
        string GetData(Nt.Data.Session session, string tableId, List<Nt.Data.Order> orders, List<Nt.Data.PaymentMethod> paymentMethods, Nt.Data.PaymentInformation paymentInformation);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId">The current client id ("1001").</param>
        /// <returns></returns>
        string GetClient(string clientId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId">The current client id ("1001").</param>
        /// <param name="posId">The current pos id ("RK2").</param>
        /// <param name="waiterId"></param>
        /// <returns></returns>
        string GetUser(string clientId, string posId, string waiterId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        object GetProvider(string clientId, string posId, string serialNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="ordersDataString"></param>
        /// <param name="paymentMethodsDataString"></param>
        /// <param name="paymentBillDataString"></param>
        object SendTransaction(Data.Session session, string ordersDataString, string paymentMethodsDataString, string paymentBillDataString);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        void CommitTransaction(Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="reason"></param>
        void RollbackTransaction(Data.Session session, string reason);
    }
}
