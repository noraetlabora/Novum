using System.Collections.Generic;

namespace Novum.Database.Api
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
        Dictionary<string, Novum.Data.PaymentType> GetPaymentTypes();
    }
}