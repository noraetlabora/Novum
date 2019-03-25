using System;
using System.Collections.Generic;

namespace Novum.Logic
{
    /// <summary>
    /// 
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public static List<Novum.Data.PaymentType> GetPaymentTypes(string department)
        {
            var paymentTypes = Novum.Database.DB.Api.Payment.GetPaymentTypes(department);
            return paymentTypes;
        }
    }
}