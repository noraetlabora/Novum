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
        /// <summary>
        /// 
        /// </summary>
        public Menu()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public List<Novum.Data.Menu> GetMainMenu(string department, string menuId)
        {
            var sql = string.Format("SELECT M.ZE, M.bez1, M.bgcolor, M.fgcolor, UM.UMENU, UM.spalten FROM  NT.TouchMenuZeile M INNER JOIN NT.TouchUmenu UM ON UM.FA = M.FA AND UM.UMENU = M.ZE WHERE M.FA = {0} AND M.MENU = '{1}'", department, menuId);
            Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": SQL = " + sql);
            var dataAdapter = new CacheDataAdapter(sql, DB.CacheConnection);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            var menus = new List<Novum.Data.Menu>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var menu = new Novum.Data.Menu();
                menu.Id = DataObject.GetString(dataRow, "UMENU");
                menu.Name = DataObject.GetString(dataRow, "bez1");
                menu.BackgroundColor = DataObject.GetString(dataRow, "bgcolor");
                menu.ForegroundColor = DataObject.GetString(dataRow, "fgcolor");
                menu.Columns = DataObject.GetUInt(dataRow, "spalten");

                menus.Add(menu);
            }

            Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": TableRowCount = " + dataTable.Rows.Count);

            return menus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public Novum.Data.Menu GetSubMenu(string department, string menuId)
        {
            var sql = string.Format("SELECT UMENU, bez, spalten FROM NT.TouchUmenu WHERE FA = {0} AND UMENU = '{1}'", department, menuId);
            Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": SQL = " + sql);
            var dataAdapter = new CacheDataAdapter(sql, DB.CacheConnection);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": TableRowCount = " + dataTable.Rows.Count);

            var menu = new Novum.Data.Menu();

            if (dataTable.Rows.Count == 0)
                return menu;

            menu.Id = DataObject.GetString(dataTable.Rows[0], "UMENU");
            menu.Name = DataObject.GetString(dataTable.Rows[0], "bez");
            menu.Name = DataObject.GetString(dataTable.Rows[0], "spalten");

            return menu;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public List<Novum.Data.MenuItem> GetMenuItems(string department, string menuId)
        {
            var sql = string.Format("SELECT ANR, ROW, COL, bez1, bgcolor, fgcolor FROM NT.TouchUMenuZeilen WHERE FA = {0} AND UMENU = '{1}'", department, menuId);
            Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": SQL = " + sql);
            var dataAdapter = new CacheDataAdapter(sql, DB.CacheConnection);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            var menuItems = new List<Novum.Data.MenuItem>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var menuItem = new Novum.Data.MenuItem();
                menuItem.Id = DataObject.GetString(dataRow, "ANR");
                menuItem.Name = DataObject.GetString(dataRow, "bez1");
                menuItem.Row = DataObject.GetUInt(dataRow, "ROW");
                menuItem.Column = DataObject.GetUInt(dataRow, "COL");
                menuItem.BackgroundColor = DataObject.GetString(dataRow, "bgcolor");
                menuItem.ForegroundColor = DataObject.GetString(dataRow, "fgcolor");

                menuItems.Add(menuItem);
            }

            Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": TableRowCount = " + dataTable.Rows.Count);

            return menuItems;
        }
    }
}