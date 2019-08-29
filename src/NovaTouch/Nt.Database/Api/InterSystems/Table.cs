using System;
using System.Collections.Generic;

namespace Nt.Database.Api.InterSystems
{
    /// <summary>
    /// 
    /// </summary>
    internal class Table : IDbTable
    {
        /// <summary>
        /// 
        /// </summary>
        public Table()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Nt.Data.Table> GetTables(Nt.Data.Session session)
        {
            var tables = new Dictionary<string, Nt.Data.Table>();
            var dbString = Interaction.CallClassMethod("cmNT.Tisch", "GetTischListe2", session.ClientId, session.PosId, session.WaiterId);
            var tablesString = new DataString(dbString);
            var tablesArray = tablesString.SplitByDoublePipes();

            foreach (string tableString in tablesArray)
            {
                if (string.IsNullOrEmpty(tableString))
                    continue;

                var table = new Nt.Data.Table();
                var dataString = new DataString(tableString);
                var dataList = new DataList(dataString.SplitByChar96());

                table.Id = dataList.GetString(0);
                table.Name = dataList.GetString(1);
                table.Amount = dataList.GetDecimal(2);
                table.Comment = dataList.GetString(3);
                table.WaiterId = dataList.GetString(7);
                table.WaiterName = dataList.GetString(8);
                table.Opend = dataList.GetDateTime(9);
                table.Updated = dataList.GetDateTime(10);
                table.Room = dataList.GetString(11);
                table.Guests = dataList.GetUInt(13);
                table.LeftTableId = dataList.GetString(21);
                table.RightTableId = dataList.GetString(22);
                table.AssignmentTypeId = dataList.GetString(23);

                tables.Add(table.Id, table);
            }

            return tables;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetTableId(Nt.Data.Session session, string tableName)
        {
            return Interaction.CallClassMethod("cmNT.Tisch", "GetTischIntern", session.ClientId, session.PosId, tableName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public string GetTableName(Nt.Data.Session session, string tableId)
        {
            return Interaction.CallClassMethod("cmNT.Tisch", "GetTischDisplay", session.ClientId, session.PosId, tableId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public string GetNewSubTableId(Nt.Data.Session session, string tableId)
        {
            return Interaction.CallClassMethod("cmNT.Tisch", "SplittTischNeu", session.ClientId, tableId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        public void OpenTable(Nt.Data.Session session, string tableId)
        {
            var dbString = Interaction.CallClassMethod("cmNT.Tisch", "TischOpen", session.ClientId, session.PosId, session.WaiterId, tableId, "0");
            var dataString = new DataString(dbString);
            var dataList = new DataList(dataString.SplitByPipe());

            var error = dataList.GetString(0).Equals("0");
            if (error)
            {
                var errorCode = dataList.GetString(1);
                var errorMessage = dataList.GetString(2);
                switch (errorCode)
                {
                    case "2":
                        throw new Exception("table not defined");
                    case "5":
                        throw new Exception("no permission to opent a table");
                    case "6":
                        throw new Exception("table already open");
                    default:
                        throw new Exception("error while opening the table");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        public void UnlockTable(Nt.Data.Session session, string tableId)
        {
            Interaction.CallClassMethod("cmNT.Tisch", "TischUnlock", session.ClientId, session.PosId, session.WaiterId, tableId);
        }
    }
}