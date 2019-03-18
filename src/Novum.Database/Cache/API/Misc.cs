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
    public class Misc : IDbMisc
    {
        public Misc()
        {
        }

        public List<Novum.Data.CancellationResason> GetCancellationReason(string department)
        {
            var sql = string.Format("SELECT * FROM NT.StornoGrund WHERE FA = {0} AND passiv > {1}", department, CacheString.SqlToday);
            Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": SQL = " + sql);
            var dataAdapter = new CacheDataAdapter(sql, DB.CacheConnection);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            var cReasons = new List<Novum.Data.CancellationResason>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var cReason = new Novum.Data.CancellationResason();
                cReason.Id = (string)dataRow["GRUND"];
                cReason.Name = (string)dataRow["bez"];
                cReasons.Add(cReason);
            }

            Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": TableRowCount = " + dataTable.Rows.Count);

            return cReasons;
        }
    }
}