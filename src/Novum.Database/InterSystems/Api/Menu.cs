using System;
using System.Text;
using System.Data;
using System.Reflection;
using Novum.Database.Api;
using Novum.Data;
using InterSystems.Data.IRISClient;
using System.Collections.Generic;

namespace Novum.Database.InterSystems.Api
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
        public Dictionary<string, Novum.Data.Menu> GetMainMenu(string menuId)
        {
            var menus = new Dictionary<string, Novum.Data.Menu>();
            var sql = string.Format("SELECT M.ZE, M.bez1, M.bgcolor, M.fgcolor, UM.UMENU, UM.spalten FROM  NT.TouchMenuZeile M INNER JOIN NT.TouchUmenu UM ON UM.FA = M.FA AND UM.UMENU = M.ZE WHERE M.FA = {0} AND M.MENU = '{1}'", Data.Department, menuId);
            var dataTable = Interaction.GetDataTable(sql);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var menu = new Novum.Data.Menu();
                menu.Id = DataObject.GetString(dataRow, "UMENU");
                menu.Name = DataObject.GetString(dataRow, "bez1");
                menu.BackgroundColor = DataObject.GetString(dataRow, "bgcolor");
                menu.ForegroundColor = DataObject.GetString(dataRow, "fgcolor");
                menu.Columns = DataObject.GetUInt(dataRow, "spalten");

                menus.Add(menu.Id, menu);
            }

            return menus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public Novum.Data.Menu GetSubMenu(string menuId)
        {
            var menu = new Novum.Data.Menu();
            var sql = string.Format("SELECT UMENU, bez, spalten FROM NT.TouchUmenu WHERE FA = {0} AND UMENU = '{1}'", Data.Department, menuId);
            var dataTable = Interaction.GetDataTable(sql);

            if (dataTable.Rows.Count == 0)
                return menu;

            menu.Id = DataObject.GetString(dataTable.Rows[0], "UMENU");
            menu.Name = DataObject.GetString(dataTable.Rows[0], "bez");
            menu.Columns = DataObject.GetUInt(dataTable.Rows[0], "spalten");

            return menu;
        }
    }
}