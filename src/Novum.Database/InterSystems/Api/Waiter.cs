using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using InterSystems.Data.CacheClient;
using Novum.Database.Api;

namespace Novum.Database.InterSystems.Api
{
    /// <summary>
    /// 
    /// </summary>
    internal class Waiter : IDbWaiter
    {
        public Waiter()
        {
        }

        public Dictionary<string, Novum.Data.Waiter> GetWaiters(string department)
        {
            var waiters = new Dictionary<string, Novum.Data.Waiter>();
            try
            {
                System.Threading.Monitor.Enter(DB.Connection);

                var sql = string.Format("SELECT PNR, name FROM NT.Pers WHERE FA = {0} AND passiv > '{1}'", department, Sql.Today);
                Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": SQL = " + sql);

                var dataAdapter = new CacheDataAdapter(sql, DB.Connection);
                var dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var waiter = new Novum.Data.Waiter();
                    waiter.Id = DataObject.GetString(dataRow, "PNR");
                    waiter.Name = DataObject.GetString(dataRow, "name");
                    waiters.Add(waiter.Id, waiter);
                }

                Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": TableRowCount = " + dataTable.Rows.Count);
            }
            catch (Exception ex)
            {
                Log.Database.Error(ex.Message);
                Log.Database.Error(ex.StackTrace);
                throw ex;
            }
            finally
            {
                System.Threading.Monitor.Exit(DB.Connection);
            }

            return waiters;
        }
    }
}