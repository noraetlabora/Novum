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
        public Dictionary<string, Novum.Data.ModifierMenu> GetModifierMenus(string department)
        {
            var modifierMenus = new Dictionary<string, Novum.Data.ModifierMenu>();
            try
            {
                System.Threading.Monitor.Enter(DB.CacheConnection);

                var sql = string.Format("SELECT UMENU, bez, aendmin, aendmax, aendauto, spalten, aendmaxaus, aendmehrfach FROM NT.TouchUmenu WHERE FA = {0} AND aend = 1", department);
                Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": SQL = " + sql);
                var dataAdapter = new CacheDataAdapter(sql, DB.CacheConnection);
                var dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

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

                Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": TableRowCount = " + dataTable.Rows.Count);
            }
            catch (Exception ex)
            {
                Log.Database.Error(ex.Message);
                Log.Database.Error(ex.StackTrace);
                throw ex;
            }
            finally
            {
                System.Threading.Monitor.Exit(DB.CacheConnection);
            }

            return modifierMenus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public Dictionary<string, Novum.Data.Modifier> GetModifiers(string department, string menuId)
        {
            var modifiers = new Dictionary<string, Novum.Data.Modifier>();
            try
            {
                System.Threading.Monitor.Enter(DB.CacheConnection);

                var sql = string.Format("SELECT M.ROW, M.COL, M.ANR, M.bgcolor, M.fgcolor, W.bez FROM NT.TouchUmenuZeilen M LEFT JOIN WW.ANR AS W ON (W.FA = M.FA AND W.ANR = M.ANR) WHERE M.FA = {0} AND M.UMENU = {1} AND M.ANR <> '' ", department, menuId);
                Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": SQL = " + sql);
                var dataAdapter = new CacheDataAdapter(sql, DB.CacheConnection);
                var dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

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

                Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": TableRowCount = " + dataTable.Rows.Count);
            }
            catch (Exception ex)
            {
                Log.Database.Error(ex.Message);
                Log.Database.Error(ex.StackTrace);
                throw ex;
            }
            finally
            {
                System.Threading.Monitor.Exit(DB.CacheConnection);
            }

            return modifiers;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public List<Novum.Data.MenuModifier> GetMenuModifiers(string department)
        {
            var menuModifiers = new List<Novum.Data.MenuModifier>();

            try
            {
                System.Threading.Monitor.Enter(DB.CacheConnection);

                var sql = string.Format("SELECT UMENU, ROW, COL, LFD, AendUMenu FROM NT.TouchUmenuZeilenA WHERE FA = {0} ", department);
                Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": SQL = " + sql);
                var dataAdapter = new CacheDataAdapter(sql, DB.CacheConnection);
                var dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

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

                Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": TableRowCount = " + dataTable.Rows.Count);
            }
            catch (Exception ex)
            {
                Log.Database.Error(ex.Message);
                Log.Database.Error(ex.StackTrace);
                throw ex;
            }
            finally
            {
                System.Threading.Monitor.Exit(DB.CacheConnection);
            }

            return menuModifiers;
        }
    }
}