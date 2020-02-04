using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<Dictionary<string, Nt.Data.PaymentType>> GetPaymentTypes();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <param name="orders"></param>
        /// <param name="paymentMethods"></param>
        /// <param name="paymentInformation"></param>
        /// <returns></returns>
        Task<Nt.Data.PaymentResult> Pay(Nt.Data.Session session, string tableId, List<Nt.Data.Order> orders, List<Nt.Data.PaymentMethod> paymentMethods, Nt.Data.PaymentInformation paymentInformation);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<string, Nt.Data.AssignmentType>> GetAssignmentTypes();

    }
}