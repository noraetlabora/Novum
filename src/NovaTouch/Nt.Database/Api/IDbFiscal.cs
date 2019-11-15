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
        /// <param name="session"></param>
        /// <returns></returns>
        string GetMode(Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        string GetModulVersion(Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        string GetConfiguration(Data.Session session);
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        string GetClient(Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        string GetUser(Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        object GetProvider(Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        string CheckSystem(Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="orders"></param>
        /// <param name="paymentMethods"></param>
        /// <param name="paymentInformation"></param>
        /// <returns></returns>
        object SendTransaction(Data.Session session, List<Nt.Data.Order> orders, List<Nt.Data.PaymentMethod> paymentMethods, Nt.Data.PaymentInformation paymentInformation);

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
