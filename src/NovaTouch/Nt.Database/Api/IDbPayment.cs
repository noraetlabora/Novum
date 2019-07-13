using System.Collections.Generic;

namespace Nt.Database.Api
{
    /// <summary>
    /// Interface for Payment Api
    /// </summary>
    public interface IDbPayment
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<string, Nt.Data.PaymentType> GetPaymentTypes();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <param name="orders"></param>
        /// <param name="paymentMethods"></param>
        /// <param name="paymentInformation"></param>
        /// <returns></returns>
        Nt.Data.PaymentResult Pay(Nt.Data.Session session, string tableId, List<Nt.Data.Order> orders, List<Nt.Data.PaymentMethod> paymentMethods, Nt.Data.PaymentInformation paymentInformation);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<string, Nt.Data.AssignmentType> GetAssignmentTypes();

        /// <summary>
        /// 0 = fiscalization
        /// 1 = no fiscalization - just payment methods where we don't need fiscalization
        /// 2 = no fiscalization due to the law
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="posId"></param>
        /// <returns></returns>
        string GetFiscalMode(string clientId, string posId);

        /// <summary>
        /// 1 = old modul version (VB6)
        /// 2 = new modul version (.NET)
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        string GetFiscalModulVersion(string clientId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        string GetFiscalServiceType(string clientId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="posId"></param>
        /// <returns></returns>
        string GetFiscalConfiguration(string clientId, string posId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <param name="orders"></param>
        /// <param name="paymentMethods"></param>
        /// <param name="paymentInformation"></param>
        /// <returns></returns>
        string GetFiscalData(Nt.Data.Session session, string tableId, List<Nt.Data.Order> orders, List<Nt.Data.PaymentMethod> paymentMethods, Nt.Data.PaymentInformation paymentInformation);
    }
}