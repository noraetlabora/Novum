using System;
using System.Data;
using System.Reflection;
using Novum.Database.API;
using Novum.Data;
using InterSystems.Data.CacheClient;
using System.Collections.Generic;

namespace Novum.Database.Cache.API
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
        public List<Novum.Data.PaymentType> GetPaymentTypes(string department)
        {
            var paymentTypes = new List<Novum.Data.PaymentType>();
            try
            {
                System.Threading.Monitor.Enter(DB.CacheConnection);

                var sql = string.Format("SELECT IKA, bez, prg, druanz, unterschrift FROM NT.Zahlart WHERE FA = {0} AND passiv > '{1}'", department, CacheString.SqlToday);
                Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": SQL = " + sql);
                var dataAdapter = new CacheDataAdapter(sql, DB.CacheConnection);
                var dataTable = new DataTable();

                dataAdapter.Fill(dataTable);

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
                    paymentTypes.Add(paymentType);
                }
                Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": TableRowCount = " + dataTable.Rows.Count);
            }
            finally
            {
                System.Threading.Monitor.Exit(DB.CacheConnection);
            }

            return paymentTypes;
        }
    }
}