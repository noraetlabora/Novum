using System;
using System.Collections.Generic;
using Nt.Database;

namespace Os.Logic
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
        public static List<Os.Data.TableResult> GetTables(Nt.Data.Session session)
        {
            var osTables = new Dictionary<string, Os.Data.TableResult>();
            var novTables = DB.Api.Table.GetTables(session);

            // create main tables
            foreach (var novTable in novTables.Values)
            {
                var osTable = new Os.Data.TableResult();
                osTable.Id = GetMainTable(novTable.Id);

                if (osTables.ContainsKey(osTable.Id))
                    continue;

                osTable.Name = GetMainTable(novTable.Name);
                osTable.SubTables = new List<Os.Data.SubTable>();
                osTable.BookedAmount = 0;
                osTable.LastActivityTime = (int)Nt.Data.Utils.Unix.Timestamp(novTable.Updated);
                osTables.Add(osTable.Id, osTable);
            }

            // add subtables to maintables
            foreach (var novTable in novTables.Values)
            {
                var osSubTable = new Os.Data.SubTable();
                osSubTable.Id = novTable.Id;
                osSubTable.Name = novTable.Name;
                osSubTable.IsSelected = false;

                var mainTableId = GetMainTable(novTable.Id);
                osTables[mainTableId].SubTables.Add(osSubTable);
                osTables[mainTableId].BookedAmount += (int)decimal.Multiply(novTable.Amount, 100.0m);

                //take time of the table last updated further in the past
                var lastUpdated = (int)Nt.Data.Utils.Unix.Timestamp(novTable.Updated);
                if (osTables[mainTableId].LastActivityTime > lastUpdated)
                    osTables[mainTableId].LastActivityTime = lastUpdated;
            }

            return new List<Os.Data.TableResult>(osTables.Values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableName"></param>
        /// <param name="serviceAreaId"></param>
        /// <param name="prePayment"></param>
        /// <returns></returns>
        public static Os.Data.TableResult OpenByName(Nt.Data.Session session, string tableName, string serviceAreaId, bool prePayment)
        {
            var osTableResult = new Os.Data.TableResult();
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
                osTableResult.SubTables = new List<Os.Data.SubTable>();
                osTableResult.BookedAmount = 0;
                osTableResult.LastActivityTime = (int)Nt.Data.Utils.Unix.Timestamp(novTable.Updated);
                osTableResult.ServiceAreaId = serviceAreaId;
            }

            // add subtables to maintable
            foreach (var novTable in novTables.Values)
            {
                novMainTableName = GetMainTable(novTable.Name);
                if (!tableName.Equals(novMainTableName))
                    continue;

                var osSubTable = new Os.Data.SubTable();
                osSubTable.Id = novTable.Id;
                osSubTable.Name = novTable.Name;
                osSubTable.IsSelected = false;

                var mainTableId = GetMainTable(novTable.Id);
                osTableResult.SubTables.Add(osSubTable);
                osTableResult.BookedAmount += (int)decimal.Multiply(novTable.Amount, 100.0m);

                //take time of the table last updated further in the past
                var lastUpdated = (int)Nt.Data.Utils.Unix.Timestamp(novTable.Updated);
                if (osTableResult.LastActivityTime > lastUpdated)
                    osTableResult.LastActivityTime = lastUpdated;
            }

            //table is not in list, create new maintable with one subtable
            if (string.IsNullOrEmpty(osTableResult.Name))
            {
                osTableResult.Id = DB.Api.Table.GetTableId(session, tableName);
                osTableResult.Name = tableName;
                osTableResult.BookedAmount = 0;
                osTableResult.LastActivityTime = (int)Nt.Data.Utils.Unix.Timestamp(DateTime.Now);

                osTableResult.SubTables = new List<Os.Data.SubTable>();
                var osSubTable = new Os.Data.SubTable();
                osSubTable.Id = osTableResult.Id;
                osSubTable.Name = osTableResult.Name;
                osSubTable.IsSelected = false;
                osTableResult.SubTables.Add(osSubTable);
            }

            DB.Api.Table.OpenTable(session, osTableResult.Id);
            //set current table in session
            var novCurrentTable = new Nt.Data.Table();
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
        public static Os.Data.SubTable CreateSubTable(Nt.Data.Session session, string tableId)
        {
            var subTable = new Os.Data.SubTable();
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
        public static Nt.Data.Table GetTable(Os.Data.TableResult osTable)
        {
            var novTable = new Nt.Data.Table();
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