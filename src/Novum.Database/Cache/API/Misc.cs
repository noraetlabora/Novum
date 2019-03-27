using System;
using System.Data;
using System.Reflection;
using System.Text;
using Novum.Database.Api;
using Novum.Data;
using InterSystems.Data.CacheClient;
using System.Collections.Generic;

namespace Novum.Database.Cache.API
{
    /// <summary>
    /// 
    /// </summary>
    internal class Misc : IDbMisc
    {
        public Misc()
        {
        }

        #region CancellationResason

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public Dictionary<string, Novum.Data.CancellationResason> GetCancellationReason(string department)
        {
            var cReasons = new Dictionary<string, Novum.Data.CancellationResason>();
            try
            {
                System.Threading.Monitor.Enter(DB.CacheConnection);

                var sql = string.Format("SELECT GRUND, bez FROM NT.StornoGrund WHERE FA = {0} AND passiv > '{1}'", department, CacheString.SqlToday);
                Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": SQL = " + sql);
                var dataAdapter = new CacheDataAdapter(sql, DB.CacheConnection);
                var dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var cReason = new Novum.Data.CancellationResason();
                    cReason.Id = DataObject.GetString(dataRow, "GRUND");
                    cReason.Name = DataObject.GetString(dataRow, "bez");
                    cReasons.Add(cReason.Id, cReason);
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
                System.Threading.Monitor.Exit(DB.CacheConnection);
            }

            return cReasons;
        }

        #endregion

        #region Service Area

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public Dictionary<string, Novum.Data.ServiceArea> GetServiceAreas(string department)
        {
            var serviceAreas = new Dictionary<string, Novum.Data.ServiceArea>();
            try
            {
                System.Threading.Monitor.Enter(DB.CacheConnection);

                var sql = string.Format("SELECT VKO, bez FROM WW.VKO WHERE FA = {0} AND passiv > '{1}'", department, CacheString.SqlToday);
                Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": SQL = " + sql);
                var dataAdapter = new CacheDataAdapter(sql, DB.CacheConnection);
                var dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var serviceArea = new Novum.Data.ServiceArea();
                    serviceArea.Id = DataObject.GetString(dataRow, "VKO");
                    serviceArea.Name = DataObject.GetString(dataRow, "bez");
                    serviceAreas.Add(serviceArea.Id, serviceArea);
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
                System.Threading.Monitor.Exit(DB.CacheConnection);
            }

            return serviceAreas;
        }
        #endregion
    }
}