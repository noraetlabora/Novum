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
        /// <param name="menuId"></param>
        /// <returns></returns>
        public Dictionary<string, Novum.Data.Menu> GetMainMenu(string menuId)
        {
            var menus = new Dictionary<string, Novum.Data.Menu>();
            var sql = new StringBuilder();
            sql.Append(" SELECT M.ZE, M.bez1, M.bgcolor, M.fgcolor, UM.UMENU, UM.spalten ");
            sql.Append(" FROM  NT.TouchMenuZeile M ");
            sql.Append(" INNER JOIN NT.TouchUmenu UM ON UM.FA = M.FA AND UM.UMENU = M.ZE ");
            sql.Append(" WHERE M.FA = ").Append(Data.ClientId);
            sql.Append(" AND M.MENU = ").Append(Interaction.SqlQuote(menuId));
            var dataTable = Interaction.GetDataTable(sql.ToString());

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
        /// <param name="menuId"></param>
        /// <returns></returns>
        public Novum.Data.Menu GetSubMenu(string menuId)
        {
            var menu = new Novum.Data.Menu();
            var sql = new StringBuilder();
            sql.Append(" SELECT UMENU, bez, spalten ");
            sql.Append(" FROM NT.TouchUmenu ");
            sql.Append(" WHERE FA = ").Append(Data.ClientId);
            sql.Append(" AND UMENU = ").Append(Interaction.SqlQuote(menuId));
            var dataTable = Interaction.GetDataTable(sql.ToString());

            if (dataTable.Rows.Count == 0)
                return menu;

            menu.Id = DataObject.GetString(dataTable.Rows[0], "UMENU");
            menu.Name = DataObject.GetString(dataTable.Rows[0], "bez");
            menu.Columns = DataObject.GetUInt(dataTable.Rows[0], "spalten");

            return menu;
        }
    }
}