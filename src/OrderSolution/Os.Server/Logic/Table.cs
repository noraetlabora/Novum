using System;
using System.Collections.Generic;
using Nt.Database;

namespace Os.Server.Logic
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
        public static List<Models.TableResult> GetTables(Nt.Data.Session session)
        {
            var osTables = new Dictionary<string, Models.TableResult>();
            var ntTables = DB.Api.Table.GetTables(session);

            // create main tables
            foreach (var ntTable in ntTables.Values)
            {
                var osTable = new Models.TableResult();
                osTable.Id = GetMainTable(ntTable.Id);

                if (osTables.ContainsKey(osTable.Id))
                    continue;

                osTable.Name = GetMainTable(ntTable.Name);
                osTable.SubTables = new List<Models.SubTable>();
                osTable.BookedAmount = 0;
                osTable.LastActivityTime = (int)Nt.Data.Utils.Unix.Timestamp(ntTable.Updated);

                if (!osTables.ContainsKey(osTable.Id))
                    osTables.Add(osTable.Id, osTable);
            }

            // add subtables to maintables
            foreach (var ntTable in ntTables.Values)
            {
                var osSubTable = new Models.SubTable();
                osSubTable.Id = ntTable.Id;
                osSubTable.Name = ntTable.Name;
                osSubTable.IsSelected = false;

                var mainTableId = GetMainTable(ntTable.Id);
                osTables[mainTableId].SubTables.Add(osSubTable);
                osTables[mainTableId].BookedAmount += (int)decimal.Multiply(ntTable.Amount, 100.0m);

                //take time of the table last updated further in the past
                var lastUpdated = (int)Nt.Data.Utils.Unix.Timestamp(ntTable.Updated);
                if (osTables[mainTableId].LastActivityTime > lastUpdated)
                    osTables[mainTableId].LastActivityTime = lastUpdated;
            }

            return new List<Models.TableResult>(osTables.Values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableName"></param>
        /// <param name="serviceAreaId"></param>
        /// <param name="prePayment"></param>
        /// <returns></returns>
        public static Models.TableResult OpenByName(Nt.Data.Session session, string tableName, string serviceAreaId, bool prePayment)
        {
            var osTableResult = new Models.TableResult();
            var ntTables = DB.Api.Table.GetTables(session);
            var ntMainTableName = "";
            //search maintable
            foreach (var novTable in ntTables.Values)
            {
                ntMainTableName = GetMainTable(novTable.Name);
                if (!tableName.Equals(ntMainTableName))
                    continue;

                osTableResult.Id = GetMainTable(novTable.Id);
                osTableResult.Name = GetMainTable(novTable.Name);
                osTableResult.SubTables = new List<Models.SubTable>();
                osTableResult.BookedAmount = 0;
                osTableResult.LastActivityTime = (int)Nt.Data.Utils.Unix.Timestamp(novTable.Updated);
                osTableResult.ServiceAreaId = serviceAreaId;
            }

            // add subtables to maintable
            foreach (var ntTable in ntTables.Values)
            {
                ntMainTableName = GetMainTable(ntTable.Name);
                if (!tableName.Equals(ntMainTableName))
                    continue;

                var osSubTable = new Models.SubTable();
                osSubTable.Id = ntTable.Id;
                osSubTable.Name = ntTable.Name;
                osSubTable.IsSelected = false;

                var mainTableId = GetMainTable(ntTable.Id);
                osTableResult.SubTables.Add(osSubTable);
                osTableResult.BookedAmount += (int)decimal.Multiply(ntTable.Amount, 100.0m);

                //take time of the table last updated further in the past
                var lastUpdated = (int)Nt.Data.Utils.Unix.Timestamp(ntTable.Updated);
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

                osTableResult.SubTables = new List<Models.SubTable>();
                var osSubTable = new Models.SubTable();
                osSubTable.Id = osTableResult.Id;
                osSubTable.Name = osTableResult.Name;
                osSubTable.IsSelected = false;
                osTableResult.SubTables.Add(osSubTable);
            }

            DB.Api.Table.OpenTable(session, osTableResult.Id);
            Table.SetCurrentTable(session, osTableResult.SubTables[0].Id);
            //
            return osTableResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public static Models.SubTable CreateSubTable(Nt.Data.Session session, string tableId)
        {
            var subTableId = DB.Api.Table.GetNewSubTableId(session, session.CurrentTable.Id);
            if (session.TableIdIsOpen(subTableId))
                subTableId = session.GetNewTableId(session.CurrentTable.Id);
            Table.SetCurrentTable(session, subTableId);
            //
            var subTable = new Models.SubTable();
            subTable.Id = subTableId;
            subTable.Name = DB.Api.Table.GetTableName(session, subTableId);
            subTable.IsSelected = false;
            //
            return subTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="osTable"></param>
        /// <returns></returns>
        public static Nt.Data.Table GetTable(Models.TableResult osTable)
        {
            var ntTable = new Nt.Data.Table();
            ntTable.Id = osTable.Id;
            ntTable.Name = osTable.Name;
            ntTable.Amount = decimal.Multiply((decimal)osTable.BookedAmount, 100.0m);
            return ntTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="subTableId"></param>
        public static void SetCurrentTable(Nt.Data.Session session, string subTableId)
        {
            var ntTable = new Nt.Data.Table();
            ntTable.Id = subTableId;
            session.SetCurrentTable(ntTable);
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