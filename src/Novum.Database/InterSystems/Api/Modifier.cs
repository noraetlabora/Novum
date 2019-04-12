using System.Data;
using Novum.Database.Api;
using System.Collections.Generic;
using System.Text;

namespace Novum.Database.InterSystems.Api
{
    /// <summary>
    /// 
    /// </summary>
    internal class Modifier : IDbModifier
    {
        /// <summary>
        /// 
        /// </summary>
        public Modifier()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public Dictionary<string, Novum.Data.ModifierMenu> GetModifierMenus()
        {
            var modifierMenus = new Dictionary<string, Novum.Data.ModifierMenu>();
            var sql = new StringBuilder();
            sql.Append(" SELECT UMENU, bez, aendmin, aendmax, aendauto, spalten, aendmaxaus, aendmehrfach ");
            sql.Append(" FROM NT.TouchUmenu ");
            sql.Append(" WHERE FA = ").Append(Data.ClientId);
            sql.Append(" AND aend = 1");
            var dataTable = Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var modifierMenu = new Novum.Data.ModifierMenu();
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
        /// <param name="department"></param>
        /// <returns></returns>
        public Dictionary<string, Novum.Data.Modifier> GetModifiers(string menuId)
        {
            var modifiers = new Dictionary<string, Novum.Data.Modifier>();
            var sql = new StringBuilder();
            sql.Append(" SELECT M.ROW, M.COL, M.ANR, M.bgcolor, M.fgcolor, W.bez ");
            sql.Append(" FROM NT.TouchUmenuZeilen M ");
            sql.Append(" LEFT JOIN WW.ANR AS W ON (W.FA = M.FA AND W.ANR = M.ANR) ");
            sql.Append(" WHERE M.FA = ").Append(Data.ClientId);
            sql.Append(" AND M.UMENU = ").Append(menuId);
            sql.Append(" AND M.ANR <> '' ");
            var dataTable = Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var modifier = new Novum.Data.Modifier();
                modifier.Id = DataObject.GetString(dataRow, "ANR");
                modifier.Name = DataObject.GetString(dataRow, "bez");
                modifier.Row = DataObject.GetUInt(dataRow, "ROW");
                modifier.Column = DataObject.GetUInt(dataRow, "COL");
                modifier.BackgroundColor = DataObject.GetString(dataRow, "bgcolor");
                modifier.ForegroundColor = DataObject.GetString(dataRow, "fgcolor");
                modifier.MinAmount = 0;
                modifier.MaxAmount = 1;

                modifiers.Add(modifier.Id, modifier);
            }

            return modifiers;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public List<Novum.Data.MenuModifier> GetMenuModifiers()
        {
            var menuModifiers = new List<Novum.Data.MenuModifier>();
            var sql = new StringBuilder();
            sql.Append(" SELECT UMENU, ROW, COL, LFD, AendUMenu ");
            sql.Append(" FROM NT.TouchUmenuZeilenA ");
            sql.Append(" WHERE FA = ").Append(Data.ClientId);
            var dataTable = Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var menuModifier = new Novum.Data.MenuModifier();

                menuModifier.MenuId = DataObject.GetString(dataRow, "UMENU");
                menuModifier.Row = DataObject.GetUInt(dataRow, "ROW");
                menuModifier.Column = DataObject.GetUInt(dataRow, "COL");
                menuModifier.Sort = DataObject.GetUInt(dataRow, "LFD");
                menuModifier.ModifierMenuId = DataObject.GetString(dataRow, "AendUMenu");

                menuModifiers.Add(menuModifier);
            }

            return menuModifiers;
        }
    }
}