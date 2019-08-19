using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using InterSystems.Data.IRISClient;
using Nt.Database.Api;

namespace Nt.Database.InterSystems.Api
{
    /// <summary>
    /// 
    /// </summary>
    internal class Menu : IDbMenu
    {
        /// <summary>
        /// 
        /// </summary>
        public Menu() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Nt.Data.Menu> GetMenus()
        {
            var menus = new Dictionary<string, Nt.Data.Menu>();
            var sql = new StringBuilder();
            sql.Append(" SELECT UMENU, bez, spalten ");
            sql.Append(" FROM NT.TouchUmenu ");
            sql.Append(" WHERE FA = ").Append(InterSystemsApi.ClientId);
            var dataTable = Interaction.GetDataTable(sql.ToString());
 
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var menu =  new Nt.Data.Menu();
                menu.Id = DataObject.GetString(dataRow, "UMENU");
                menu.Name = DataObject.GetString(dataRow, "bez");
                menu.Columns = DataObject.GetUInt(dataRow, "spalten");

                if (!menus.ContainsKey(menu.Id))
                    menus.Add(menu.Id, menu);
            }

            return menus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="posId"></param>
        /// <returns></returns>
        public string GetMenuId(string posId)
        {
            return Interaction.CallClassMethod("cmNT.Kassa", "GetTouchMenu", InterSystemsApi.ClientId, posId, "0");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public Dictionary<string, Nt.Data.Menu> GetMainMenus(string menuId)
        {
            var menus = new Dictionary<string, Nt.Data.Menu>();
            var sql = new StringBuilder();
            sql.Append(" SELECT M.ZE, M.bez1, M.bgcolor, M.fgcolor, UM.UMENU, UM.spalten ");
            sql.Append(" FROM  NT.TouchMenuZeile M ");
            sql.Append(" INNER JOIN NT.TouchUmenu UM ON UM.FA = M.FA AND UM.UMENU = M.UMENU ");
            sql.Append(" WHERE M.FA = ").Append(InterSystemsApi.ClientId);
            sql.Append(" AND M.MENU = ").Append(Interaction.SqlQuote(menuId));
            var dataTable = Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var menu = new Nt.Data.Menu();
                menu.Id = DataObject.GetString(dataRow, "UMENU");
                menu.Name = DataObject.GetString(dataRow, "bez1");
                menu.BackgroundColor = DataObject.GetString(dataRow, "bgcolor");
                menu.ForegroundColor = DataObject.GetString(dataRow, "fgcolor");
                menu.Columns = DataObject.GetUInt(dataRow, "spalten");
                if (!menus.ContainsKey(menu.Id))
                    menus.Add(menu.Id, menu);
            }

            return menus;
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Nt.Data.MenuItem> GetMenuItems() {
            var menuItems = new List<Nt.Data.MenuItem>();
            var sql = new StringBuilder();
            sql.Append("  SELECT DISTINCT A.Anr, A.UMENU  ");
            sql.Append(" , (SELECT MIN(B.ROW) FROM NT.TouchUMenuZeilen B WHERE B.FA = A.FA AND B.UMENU = A.UMENU AND B.Anr = A.Anr) As FromRow ");
            sql.Append(" , (SELECT MAX(C.ROW) FROM NT.TouchUMenuZeilen C WHERE C.FA = A.FA AND C.UMENU = A.UMENU AND C.Anr = A.Anr) As ToRow ");
            sql.Append(" , (SELECT MIN(D.COL) FROM NT.TouchUMenuZeilen D WHERE D.FA = A.FA AND D.UMENU = A.UMENU AND D.Anr = A.Anr) As FromCol ");
            sql.Append(" , (SELECT MAX(E.COL) FROM NT.TouchUMenuZeilen E WHERE E.FA = A.FA AND E.UMENU = A.UMENU AND E.Anr = A.Anr) As ToCol ");
            sql.Append(" , cmNT.BonTouch_GetArtikelTouchBezeichnung(A.FA,'RK',A.UMENU,A.ROW, A.COL,0,1) As name");
            sql.Append(" FROM NT.TouchUMenuZeilen A ");
            sql.Append(" WHERE A.FA = ").Append(InterSystemsApi.ClientId);
            var dataTable = Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var menuItem = new Nt.Data.MenuItem();
                menuItem.ArticleId = DataObject.GetString(dataRow, "Anr");
                menuItem.Name = DataObject.GetString(dataRow, "name");
                menuItem.MenuId = DataObject.GetString(dataRow, "UMENU");
                menuItem.FromRow = DataObject.GetUInt(dataRow, "FromRow");
                menuItem.ToRow = DataObject.GetUInt(dataRow, "ToRow");
                menuItem.FromColumn = DataObject.GetUInt(dataRow, "FromCol");
                menuItem.ToColumn = DataObject.GetUInt(dataRow, "ToCol");
                
                menuItems.Add(menuItem);
            }

            return menuItems;
        }        
    }
}