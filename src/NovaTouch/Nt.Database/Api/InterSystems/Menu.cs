using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Database.Api.InterSystems
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
        public async Task<Dictionary<string, Nt.Data.Menu>> GetMenus()
        {
            var menus = new Dictionary<string, Nt.Data.Menu>();
            var sql = new StringBuilder();
            sql.Append(" SELECT UMENU, bez, spalten ");
            sql.Append(" FROM NT.TouchUmenu ");
            sql.Append(" WHERE FA = ").Append(Api.ClientId);
            var dataTable = await Intersystems.Instance.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var menu = new Nt.Data.Menu();
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
        public async Task<string> GetMenuId(string posId)
        {
            return await Intersystems.Instance.CallClassMethod("cmNT.Kassa", "GetTouchMenu", Api.ClientId, posId, "0");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, Nt.Data.Menu>> GetMainMenus(string menuId)
        {
            var menus = new Dictionary<string, Nt.Data.Menu>();
            var sql = new StringBuilder();
            sql.Append(" SELECT M.ZE, M.bez1, M.bgcolor, M.fgcolor, UM.UMENU, UM.spalten ");
            sql.Append(" FROM  NT.TouchMenuZeile M ");
            sql.Append(" INNER JOIN NT.TouchUmenu UM ON UM.FA = M.FA AND UM.UMENU = M.UMENU ");
            sql.Append(" WHERE M.FA = ").Append(Api.ClientId);
            sql.Append(" AND M.MENU = ").Append(Intersystems.SqlQuote(menuId));
            var dataTable = await Intersystems.Instance.GetDataTable(sql.ToString());

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
        public async Task<List<Nt.Data.MenuItem>> GetMenuItems()
        {
            var menuItems = new List<Nt.Data.MenuItem>();
            var sql = new StringBuilder();
            sql.Append("  SELECT DISTINCT A.Anr, A.UMENU  ");
            sql.Append(" , (SELECT MIN(B.ROW) FROM NT.TouchUMenuZeilen B WHERE B.FA = A.FA AND B.UMENU = A.UMENU AND B.Anr = A.Anr) As FromRow ");
            sql.Append(" , (SELECT MAX(C.ROW) FROM NT.TouchUMenuZeilen C WHERE C.FA = A.FA AND C.UMENU = A.UMENU AND C.Anr = A.Anr) As ToRow ");
            sql.Append(" , (SELECT MIN(D.COL) FROM NT.TouchUMenuZeilen D WHERE D.FA = A.FA AND D.UMENU = A.UMENU AND D.Anr = A.Anr) As FromCol ");
            sql.Append(" , (SELECT MAX(E.COL) FROM NT.TouchUMenuZeilen E WHERE E.FA = A.FA AND E.UMENU = A.UMENU AND E.Anr = A.Anr) As ToCol ");
            sql.Append(" , cmNT.BonTouch_GetArtikelTouchBezeichnung(A.FA,'RK',A.UMENU,A.ROW, A.COL,0,1) As name");
            sql.Append(" FROM NT.TouchUMenuZeilen A ");
            sql.Append(" WHERE A.FA = ").Append(Api.ClientId);
            var dataTable = await Intersystems.Instance.GetDataTable(sql.ToString());

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