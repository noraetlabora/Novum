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
    }
}