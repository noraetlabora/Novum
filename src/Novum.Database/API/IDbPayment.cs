using System.Collections.Generic;

namespace Novum.Database.API
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbPayment
    {
        List<Novum.Data.PaymentType> GetPaymentTypes(string department);
    }
}