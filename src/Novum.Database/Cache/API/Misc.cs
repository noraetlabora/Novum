using System;
using System.Data;
using System.Reflection;
using System.Text;
using Novum.Database.API;
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
        public List<Novum.Data.CancellationResason> GetCancellationReason(string department)
        {
            var cReasons = new List<Novum.Data.CancellationResason>();
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
                    cReasons.Add(cReason);
                }

                Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": TableRowCount = " + dataTable.Rows.Count);
            }
            finally
            {
                System.Threading.Monitor.Exit(DB.CacheConnection);
            }

            return cReasons;
        }

        #endregion

        #region Article

        public List<Novum.Data.MenuItem> GetArticles(string department)
        {
            var menuItems = new List<Novum.Data.MenuItem>();
            try
            {
                System.Threading.Monitor.Enter(DB.CacheConnection);

                var sql = new StringBuilder();
                sql.Append("SELECT M.Anr, M.ROW, M.COL, M.bez1, M.bgcolor, M.fgcolor, A.vkaend, A.nameaend, P.PLU ");
                sql.Append("FROM NT.TouchUMenuZeilen M ");
                sql.Append("LEFT JOIN WW.ANRKassa AS A ON (A.FA=M.FA AND A.ANR=M.ANR) ");
                sql.Append("LEFT JOIN NT.PLUTabDet AS P ON (P.FA = M.FA AND P.ANR = M.ANR) ");
                sql.Append("WHERE FA = ").Append(department);
                sql.Append("AND   ISNUMERIC(M.Anr) = 1");
                Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": SQL = " + sql.ToString());
                var dataAdapter = new CacheDataAdapter(sql.ToString(), DB.CacheConnection);
                var dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var menuItem = new Novum.Data.MenuItem();
                    menuItem.Id = DataObject.GetString(dataRow, "Anr");
                    menuItem.Row = DataObject.GetUInt(dataRow, "ROW");
                    menuItem.Column = DataObject.GetUInt(dataRow, "COL");
                    menuItem.Name = DataObject.GetString(dataRow, "bez1");
                    menuItem.BackgroundColor = DataObject.GetString(dataRow, "bgcolor");
                    menuItem.ForegroundColor = DataObject.GetString(dataRow, "fgcolor");
                    menuItem.AskForPrice = DataObject.GetBool(dataRow, "vkaend");
                    menuItem.AskForName = DataObject.GetBool(dataRow, "nameaend");
                    menuItem.PLU = DataObject.GetString(dataRow, "PLU");
                    menuItems.Add(menuItem);
                }

                Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": TableRowCount = " + dataTable.Rows.Count);
            }
            finally
            {
                System.Threading.Monitor.Exit(DB.CacheConnection);
            }

            return menuItems;
        }

        #endregion

    }
}