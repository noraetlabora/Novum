using System.Collections.Generic;

namespace Novum.Database.Api
{
    /// <summary>
    /// Interface for Payment Api
    /// </summary>
    public interface IDbPayment
    {
        Dictionary<string, Novum.Data.PaymentType> GetPaymentTypes(string department);
    }
}