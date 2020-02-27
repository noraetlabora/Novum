using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<string> GetMode(Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        Task<string> GetModulVersion(Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        Task<string> GetConfiguration(Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        Task<string> GetClient(Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        Task<string> GetUser(Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        Task<object> GetProvider(Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        Task<string> CheckSystem(Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="orders"></param>
        /// <param name="paymentMethods"></param>
        /// <param name="paymentInformation"></param>
        /// <returns></returns>
        Task<object> SendTransaction(Data.Session session, List<Nt.Data.Order> orders, List<Nt.Data.PaymentMethod> paymentMethods, Nt.Data.PaymentInformation paymentInformation);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        Task CommitTransaction(Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="reason"></param>
        Task RollbackTransaction(Data.Session session, string reason);
    }
}
