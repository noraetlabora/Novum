using System.Data;
using Novum.Database.Api;
using System.Collections.Generic;

namespace Novum.Database.InterSystems.Api
{
    /// <summary>
    /// 
    /// </summary>
    internal class Payment : IDbPayment
    {
        public Payment()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public Dictionary<string, Novum.Data.PaymentType> GetPaymentTypes()
        {
            var paymentTypes = new Dictionary<string, Novum.Data.PaymentType>();
            var sql = string.Format("SELECT IKA, bez, prg, druanz, unterschrift FROM NT.Zahlart WHERE FA = {0} AND passiv > '{1}'", Data.Department, Interaction.SqlToday);
            var dataTable = Interaction.GetDataTable(sql);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var paymentType = new Novum.Data.PaymentType();
                paymentType.Id = DataObject.GetString(dataRow, "IKA");
                paymentType.Name = DataObject.GetString(dataRow, "bez");
                paymentType.Program = DataObject.GetString(dataRow, "prg");
                paymentType.ReceiptCount = DataObject.GetUInt(dataRow, "druanz");
                var signature = DataObject.GetString(dataRow, "unterschrift");
                if (signature.Equals("0"))
                    paymentType.Signature = true;
                else
                    paymentType.Signature = false;
                paymentTypes.Add(paymentType.Id, paymentType);
            }

            return paymentTypes;
        }
    }
}