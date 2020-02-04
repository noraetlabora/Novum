using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Database.Api.InterSystems
{
    /// <summary>
    /// 
    /// </summary>
    internal class Modifier : IDbModifier
    {
        /// <summary>
        /// 
        /// </summary>
        public Modifier() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, Nt.Data.ModifierMenu>> GetModifierMenus()
        {
            var modifierMenus = new Dictionary<string, Nt.Data.ModifierMenu>();
            var sql = new StringBuilder();
            sql.Append(" SELECT UMENU, bez, aendmin, aendmax, aendauto, spalten, aendmaxaus, aendmehrfach ");
            sql.Append(" FROM NT.TouchUmenu ");
            sql.Append(" WHERE FA = ").Append(Api.ClientId);
            sql.Append(" AND aend = 1");
            var dataTable = await Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var modifierMenu = new Nt.Data.ModifierMenu();
                modifierMenu.Id = DataObject.GetString(dataRow, "UMENU");
                modifierMenu.Name = DataObject.GetString(dataRow, "bez");
                modifierMenu.MinSelection = DataObject.GetUInt(dataRow, "aendmin");
                modifierMenu.MaxSelection = DataObject.GetUInt(dataRow, "aendmax");
                modifierMenu.Columns = DataObject.GetUInt(dataRow, "spalten");

                modifierMenus.Add(modifierMenu.Id, modifierMenu);
            }

            return modifierMenus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modifierMenuId"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, Nt.Data.ModifierItem>> GetModifierItems(string modifierMenuId)
        {
            var modifierItems = new Dictionary<string, Nt.Data.ModifierItem>();
            var sql = new StringBuilder();
            sql.Append(" SELECT M.ROW, M.COL, M.ANR, M.bgcolor, M.fgcolor, W.vkbez ");
            sql.Append(" FROM NT.TouchUmenuZeilen M ");
            sql.Append(" LEFT JOIN WW.ANR AS W ON (W.FA = M.FA AND W.ANR = M.ANR) ");
            sql.Append(" WHERE M.FA = ").Append(Api.ClientId);
            sql.Append(" AND M.UMENU = ").Append(modifierMenuId);
            sql.Append(" AND M.ANR <> '' ");
            var dataTable = await Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var modifierItem = new Nt.Data.ModifierItem();
                modifierItem.Id = DataObject.GetString(dataRow, "ANR");
                modifierItem.Name = DataObject.GetString(dataRow, "vkbez");
                modifierItem.Row = DataObject.GetUInt(dataRow, "ROW");
                modifierItem.Column = DataObject.GetUInt(dataRow, "COL");
                modifierItem.BackgroundColor = DataObject.GetString(dataRow, "bgcolor");
                modifierItem.ForegroundColor = DataObject.GetString(dataRow, "fgcolor");
                modifierItem.MinAmount = 0;
                modifierItem.MaxAmount = 1;

                if (!modifierItems.ContainsKey(modifierItem.Id))
                    modifierItems.Add(modifierItem.Id, modifierItem);
            }

            return modifierItems;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Nt.Data.MenuItemModifierMenu>> GetMenuItemModifierMenus()
        {
            var menus = new List<Nt.Data.MenuItemModifierMenu>();
            var sql = new StringBuilder();
            sql.Append(" SELECT UMENU, ROW, COL, LFD, AendUMenu ");
            sql.Append(" FROM NT.TouchUmenuZeilenA ");
            sql.Append(" WHERE FA = ").Append(Api.ClientId);
            var dataTable = await Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var menu = new Nt.Data.MenuItemModifierMenu();

                menu.MenuItemMenuId = DataObject.GetString(dataRow, "UMENU");
                menu.MenuItemRow = DataObject.GetUInt(dataRow, "ROW");
                menu.MenuItemColumn = DataObject.GetUInt(dataRow, "COL");
                menu.Sort = DataObject.GetUInt(dataRow, "LFD");
                menu.ModifierMenuId = DataObject.GetString(dataRow, "AendUMenu");

                menus.Add(menu);
            }

            return menus;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="articleId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public async Task<Nt.Data.Modifier> GetModifier(Nt.Data.Session session, string articleId, decimal quantity)
        {
            var modifier = new Nt.Data.Modifier();
            var dbString = await Interaction.CallClassMethod("cmNT.BonOman", "GetArtikelDaten", session.ClientId, session.PosId, session.WaiterId, "tableId", session.PriceLevel, "N", "", "", "", articleId, "", quantity);
            var dataString = new DataString(dbString);
            var dataList = new DataList(dataString.SplitByChar96());

            modifier.ArticleId = dataList.GetString(0);
            modifier.Name = dataList.GetString(1);
            modifier.Quantity = quantity;
            modifier.MenuId = dataList.GetString(29);

            var priceString = dataList.GetString(4);
            if (priceString.Contains("%"))
            {
                var priceDataString = new DataString(priceString);
                var priceDataList = new DataList(priceDataString.SplitByPercent());
                modifier.Percent = priceDataList.GetDecimal(0);
                modifier.Rounding = priceDataList.GetDecimal(1);
            }
            else
            {
                modifier.UnitPrice = dataList.GetDecimal(4);
            }

            return modifier;
        }
    }
}