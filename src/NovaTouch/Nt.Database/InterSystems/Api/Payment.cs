using System.Collections.Generic;
using System.Data;
using System.Text;
using Nt.Database.Api;

namespace Nt.Database.InterSystems.Api
{
    /// <summary>
    /// 
    /// </summary>
    internal class Payment : IDbPayment
    {
        public Payment() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Nt.Data.PaymentType> GetPaymentTypes()
        {
            var paymentTypes = new Dictionary<string, Nt.Data.PaymentType>();
            var sql = new StringBuilder();
            sql.Append(" SELECT IKA, bez, prg, druanz, unterschrift ");
            sql.Append(" FROM NT.Zahlart ");
            sql.Append(" WHERE FA = ").Append(Data.ClientId);
            sql.Append(" AND passiv > ").Append(Interaction.SqlToday);
            var dataTable = Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var paymentType = new Nt.Data.PaymentType();
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