using System.Collections.Generic;

namespace Novum.Database.Api
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbPayment
    {
        Dictionary<string, Novum.Data.PaymentType> GetPaymentTypes(string department);
    }
}