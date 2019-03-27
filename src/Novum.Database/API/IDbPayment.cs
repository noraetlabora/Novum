using System.Collections.Generic;

namespace Novum.Database.API
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbPayment
    {
        Dictionary<string, Novum.Data.PaymentType> GetPaymentTypes(string department);
    }
}