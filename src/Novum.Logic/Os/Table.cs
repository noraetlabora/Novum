using System;
using System.Collections.Generic;
using Novum.Data;
using Novum.Data.Os;
using Novum.Database;


namespace Novum.Logic.Os
{

    /// <summary>
    /// 
    /// </summary>
    public class Table
    {

        #region public static methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<TableResult> GetTables(Session session)
        {
            var osTables = new Dictionary<string, TableResult>();
            var novTables = DB.Api.Table.GetTables(session);

            // create main tables
            foreach (var novTable in novTables.Values)
            {
                var osTable = new TableResult();
                osTable.Id = GetMainTable(novTable.Id);

                if (osTables.ContainsKey(osTable.Id))
                    continue;

                osTable.Name = GetMainTable(novTable.Name);
                osTable.SubTables = new List<SubTable>();
                osTable.BookedAmount = 0;
                osTable.LastActivityTime = (int)Utils.Unix.Timestamp(novTable.Updated);
                osTables.Add(osTable.Id, osTable);
            }

            // add subtables to maintables
            foreach (var novTable in novTables.Values)
            {
                var osSubTable = new SubTable();
                osSubTable.Id = novTable.Id;
                osSubTable.Name = novTable.Name;
                osSubTable.IsSelected = false;

                var mainTableId = GetMainTable(novTable.Id);
                osTables[mainTableId].SubTables.Add(osSubTable);
                osTables[mainTableId].BookedAmount += (int)decimal.Multiply(novTable.Amount, 100.0m);

                //take time of the table last updated further in the past
                var lastUpdated = (int)Utils.Unix.Timestamp(novTable.Updated);
                if (osTables[mainTableId].LastActivityTime > lastUpdated)
                    osTables[mainTableId].LastActivityTime = lastUpdated;
            }

            return new List<TableResult>(osTables.Values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableName"></param>
        /// <param name="serviceAreaId"></param>
        /// <param name="prePayment"></param>
        /// <returns></returns>
        public static TableResult OpenByName(Session session, string tableName, string serviceAreaId, bool prePayment)
        {
            var osTableResult = new TableResult();
            var novTables = DB.Api.Table.GetTables(session);
            var novMainTableName = "";
            //search maintable
            foreach (var novTable in novTables.Values)
            {
                novMainTableName = GetMainTable(novTable.Name);
                if (!tableName.Equals(novMainTableName))
                    continue;

                osTableResult.Id = GetMainTable(novTable.Id);
                osTableResult.Name = GetMainTable(novTable.Name);
                osTableResult.SubTables = new List<SubTable>();
                osTableResult.BookedAmount = 0;
                osTableResult.LastActivityTime = (int)Utils.Unix.Timestamp(novTable.Updated);
                osTableResult.ServiceAreaId = serviceAreaId;
            }

            // add subtables to maintable
            foreach (var novTable in novTables.Values)
            {
                novMainTableName = GetMainTable(novTable.Name);
                if (!tableName.Equals(novMainTableName))
                    continue;

                var osSubTable = new SubTable();
                osSubTable.Id = novTable.Id;
                osSubTable.Name = novTable.Name;
                osSubTable.IsSelected = false;

                var mainTableId = GetMainTable(novTable.Id);
                osTableResult.SubTables.Add(osSubTable);
                osTableResult.BookedAmount += (int)decimal.Multiply(novTable.Amount, 100.0m);

                //take time of the table last updated further in the past
                var lastUpdated = (int)Utils.Unix.Timestamp(novTable.Updated);
                if (osTableResult.LastActivityTime > lastUpdated)
                    osTableResult.LastActivityTime = lastUpdated;
            }

            //table is not in list, create new maintable with one subtable
            if (string.IsNullOrEmpty(osTableResult.Name))
            {
                osTableResult.Id = DB.Api.Table.GetTableId(session, tableName);
                osTableResult.Name = tableName;
                osTableResult.BookedAmount = 0;
                osTableResult.LastActivityTime = (int)Utils.Unix.Timestamp(DateTime.Now);

                osTableResult.SubTables = new List<SubTable>();
                var osSubTable = new SubTable();
                osSubTable.Id = osTableResult.Id;
                osSubTable.Name = osTableResult.Name;
                osSubTable.IsSelected = false;
                osTableResult.SubTables.Add(osSubTable);
            }

            DB.Api.Table.OpenTable(session, osTableResult.Id);
            //set current table in session
            var novCurrentTable = new Novum.Data.Table();
            novCurrentTable.Id = osTableResult.SubTables[0].Id;
            novCurrentTable.Name = osTableResult.SubTables[0].Name;
            session.SetCurrentTable(novCurrentTable);
            //
            return osTableResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public static SubTable CreateSubTable(Session session, string tableId)
        {
            var subTable = new SubTable();
            subTable.Id = DB.Api.Table.GetNewSubTableId(session, tableId);
            subTable.Name = DB.Api.Table.GetTableName(session, subTable.Id);
            subTable.IsSelected = false;
            return subTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="osTable"></param>
        /// <returns></returns>
        public static Novum.Data.Table GetTable(Novum.Data.Os.TableResult osTable)
        {
            var novTable = new Novum.Data.Table();
            novTable.Id = osTable.Id;
            novTable.Name = osTable.Name;
            novTable.Amount = decimal.Multiply((decimal)osTable.BookedAmount, 100.0m);
            return novTable;
        }

        #endregion

        #region private static methods

        /// <summary>
        ///  returns the main table, it does not check if the string is an id or name of the table
        /// </summary>
        /// <param name="table"></param>
        /// <returns>1010 (table 1010), 1010 (table 1010.1), 10 (table 10) 10 (table 10.2)</returns>
        private static string GetMainTable(string table)
        {
            if (table.Contains("."))
            {
                var index = table.IndexOf(".");
                return table.Substring(0, index);
            }
            return table;
        }

        #endregion
    }
}
