using System.Collections.Generic;
using System.Data;
using System.Text;
using Nt.Database.Api;

namespace Nt.Database.InterSystems.Api
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
        public Dictionary<string, Nt.Data.ModifierMenu> GetModifierMenus()
        {
            var modifierMenus = new Dictionary<string, Nt.Data.ModifierMenu>();
            var sql = new StringBuilder();
            sql.Append(" SELECT UMENU, bez, aendmin, aendmax, aendauto, spalten, aendmaxaus, aendmehrfach ");
            sql.Append(" FROM NT.TouchUmenu ");
            sql.Append(" WHERE FA = ").Append(InterSystemsApi.ClientId);
            sql.Append(" AND aend = 1");
            var dataTable = Interaction.GetDataTable(sql.ToString());

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
        /// <param name="menuId"></param>
        /// <returns></returns>
        public Dictionary<string, Nt.Data.Modifier> GetModifiers(string menuId)
        {
            var modifiers = new Dictionary<string, Nt.Data.Modifier>();
            var sql = new StringBuilder();
            sql.Append(" SELECT M.ROW, M.COL, M.ANR, M.bgcolor, M.fgcolor, W.bez ");
            sql.Append(" FROM NT.TouchUmenuZeilen M ");
            sql.Append(" LEFT JOIN WW.ANR AS W ON (W.FA = M.FA AND W.ANR = M.ANR) ");
            sql.Append(" WHERE M.FA = ").Append(InterSystemsApi.ClientId);
            sql.Append(" AND M.UMENU = ").Append(menuId);
            sql.Append(" AND M.ANR <> '' ");
            var dataTable = Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var modifier = new Nt.Data.Modifier();
                modifier.Id = DataObject.GetString(dataRow, "ANR");
                modifier.Name = DataObject.GetString(dataRow, "bez");
                modifier.Row = DataObject.GetUInt(dataRow, "ROW");
                modifier.Column = DataObject.GetUInt(dataRow, "COL");
                modifier.BackgroundColor = DataObject.GetString(dataRow, "bgcolor");
                modifier.ForegroundColor = DataObject.GetString(dataRow, "fgcolor");
                modifier.MinAmount = 0;
                modifier.MaxAmount = 1;

                if (!modifiers.ContainsKey(modifier.Id))
                    modifiers.Add(modifier.Id, modifier);
            }

            return modifiers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Nt.Data.MenuItemModifierMenu> GetMenuItemModifierMenus()
        {
            var menus = new List<Nt.Data.MenuItemModifierMenu>();
            var sql = new StringBuilder();
            sql.Append(" SELECT UMENU, ROW, COL, LFD, AendUMenu ");
            sql.Append(" FROM NT.TouchUmenuZeilenA ");
            sql.Append(" WHERE FA = ").Append(InterSystemsApi.ClientId);
            var dataTable = Interaction.GetDataTable(sql.ToString());

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
    }
}