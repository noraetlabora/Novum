using Nt.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<Dictionary<string, Nt.Data.Table>> GetTables(Nt.Data.Session session)
        {
            var tables = new Dictionary<string, Nt.Data.Table>();
            var dbString = await InterSystems.CallClassMethod("cmNT.Tisch", "GetTischListe2", session.ClientId, session.PosId, session.WaiterId).ConfigureAwait(false);
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
        public Task<string> GetTableId(Nt.Data.Session session, string tableName)
        {
            return InterSystems.CallClassMethod("cmNT.Tisch", "GetTischIntern", session.ClientId, session.PosId, tableName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public Task<string> GetTableName(Nt.Data.Session session, string tableId)
        {
            return InterSystems.CallClassMethod("cmNT.Tisch", "GetTischDisplay", session.ClientId, session.PosId, tableId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public Task<string> GetNewSubTableId(Nt.Data.Session session, string tableId)
        {
            return InterSystems.CallClassMethod("cmNT.Tisch", "SplittTischNeu", session.ClientId, tableId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        public async Task OpenTable(Nt.Data.Session session, string tableId)
        {
            var dbString = await InterSystems.CallClassMethod("cmNT.Tisch", "TischOpen", session.ClientId, session.PosId, session.WaiterId, tableId, "0").ConfigureAwait(false);
            var dataString = new DataString(dbString);
            var dataList = new DataList(dataString.SplitByPipe());

            var error = dataList.GetString(0).Equals("0");
            if (error)
            {
                var errorCode = dataList.GetString(1);
                var errorMessage = dataList.GetString(2);
                var tableName = GetTableName(session, tableId);
                switch (errorCode)
                {
                    case "2":
                        throw new Exception(string.Format(Resources.Dictionary.GetString("Table_NotDefined"), tableName));
                    case "5":
                        throw new Exception(string.Format(Resources.Dictionary.GetString("Table_NoOpenPermission"), tableName));
                    case "6":
                        throw new Exception(string.Format(Resources.Dictionary.GetString("Tabe_AlreadyOpen"), tableName));
                    default:
                        throw new Exception(string.Format(Resources.Dictionary.GetString("Table_OpenError"), tableName));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        public Task UnlockTable(Nt.Data.Session session, string tableId)
        {
            return InterSystems.CallClassMethod("cmNT.Tisch", "TischUnlock", session.ClientId, session.PosId, session.WaiterId, tableId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="sourceTableId"></param>
        /// <param name="targetTableId"></param>
        public async Task SplitStart(Session session, string sourceTableId, string targetTableId)
        {
            await InterSystems.CallVoidClassMethod("cmNT.SplittOman", "SetSplittStart", session.ClientId, session.PosId, session.SerialNumber, session.WaiterId).ConfigureAwait(false);
            await InterSystems.CallVoidClassMethod("cmNT.SplittOman", "SetSplittDaten", session.ClientId, session.PosId, session.SerialNumber, session.WaiterId, sourceTableId, targetTableId).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="sourceTableId"></param>
        /// <param name="targetTableId"></param>
        /// <param name="order"></param>
        /// <param name="quantity"></param>
        public async Task SplitOrder(Session session, string sourceTableId, string targetTableId, Data.Order order, decimal quantity)
        {
            var orderDataString = Order.GetOrderDataString(order);
            var returnValue = await InterSystems.CallClassMethod("cmNT.SplittOman", "SetSplittZeile", session.ClientId, session.PosId, session.SerialNumber, session.WaiterId, sourceTableId, targetTableId, orderDataString, orderDataString, quantity).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public Task SplitDone(Session session)
        {
            return InterSystems.CallVoidClassMethod("cmNT.SplittOman", "SetSplittOK", session.ClientId, session.PosId, session.SerialNumber, session.WaiterId);
        }
    }
}