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
    internal class Menu : IDbMenu
    {
        public Menu()
        {
        }

        public List<string> GetCategories(string department, string menuId)
        {
            var sql = string.Format("SELECT * FROM NT.TouchMenuZeile WHERE FA = {0} AND MENU = '{1}'", department, menuId);
            Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": SQL = " + sql);
            var dataAdapter = new CacheDataAdapter(sql, DB.CacheConnection);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            var categories = new List<string>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var category = "";
                category = (string)dataRow["ZE"];
            }

            Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": TableRowCount = " + dataTable.Rows.Count);

            return categories;
        }
    }
}