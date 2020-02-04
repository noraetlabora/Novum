using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nt.Data;

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
            var dbString = await Interaction.CallClassMethod("cmNT.Tisch", "GetTischListe2", session.ClientId, session.PosId, session.WaiterId);
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
        public async Task<string> GetTableId(Nt.Data.Session session, string tableName)
        {
            return await Interaction.CallClassMethod("cmNT.Tisch", "GetTischIntern", session.ClientId, session.PosId, tableName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public async Task<string> GetTableName(Nt.Data.Session session, string tableId)
        {
            return await Interaction.CallClassMethod("cmNT.Tisch", "GetTischDisplay", session.ClientId, session.PosId, tableId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public async Task<string> GetNewSubTableId(Nt.Data.Session session, string tableId)
        {
            return await Interaction.CallClassMethod("cmNT.Tisch", "SplittTischNeu", session.ClientId, tableId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        public async Task OpenTable(Nt.Data.Session session, string tableId)
        {
            var dbString = await Interaction.CallClassMethod("cmNT.Tisch", "TischOpen", session.ClientId, session.PosId, session.WaiterId, tableId, "0");
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
                        throw new Exception(string.Format(Resources.Dictionary.GetString("Table_NoOpenPermission"),tableName));
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
        public async Task UnlockTable(Nt.Data.Session session, string tableId)
        {
            await Interaction.CallClassMethod("cmNT.Tisch", "TischUnlock", session.ClientId, session.PosId, session.WaiterId, tableId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="sourceTableId"></param>
        /// <param name="targetTableId"></param>
        public async Task SplitStart(Session session, string sourceTableId, string targetTableId)
        {
            await Interaction.CallVoidClassMethod("cmNT.SplittOman", "SetSplittStart", session.ClientId, session.PosId, session.SerialNumber, session.WaiterId);
            await Interaction.CallVoidClassMethod("cmNT.SplittOman", "SetSplittDaten", session.ClientId, session.PosId, session.SerialNumber, session.WaiterId, sourceTableId, targetTableId);
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
            var returnValue = await Interaction.CallClassMethod("cmNT.SplittOman", "SetSplittZeile", session.ClientId, session.PosId, session.SerialNumber, session.WaiterId, sourceTableId, targetTableId, orderDataString, orderDataString, quantity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public async Task SplitDone(Session session)
        {
            await Interaction.CallVoidClassMethod("cmNT.SplittOman", "SetSplittOK", session.ClientId, session.PosId, session.SerialNumber, session.WaiterId);
        }
    }
}