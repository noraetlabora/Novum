using Nt.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nt.Database.Api.Intersystems
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
            var args = new object[3] { session.ClientId, session.PosId, session.WaiterId };
            var dbString = await Intersystems.CallClassMethod("cmNT.Tisch", "GetTischListe2", args).ConfigureAwait(false);
            var tablesString = new DataString(dbString);
            var tablesArray = tablesString.SplitByDoublePipes();

            foreach (string tableString in tablesArray)
            {
                if (string.IsNullOrEmpty(tableString))
                    continue;

                var table = new Nt.Data.Table();
                var dataString = new DataString(tableString);
                var dataArray = new DataArray(dataString.SplitByChar96());

                table.Id = dataArray.GetString(0);
                table.Name = dataArray.GetString(1);
                table.Amount = dataArray.GetDecimal(2);
                table.Comment = dataArray.GetString(3);
                table.WaiterId = dataArray.GetString(7);
                table.WaiterName = dataArray.GetString(8);
                table.Opend = dataArray.GetDateTime(9);
                table.Updated = dataArray.GetDateTime(10);
                table.Room = dataArray.GetString(11);
                table.Guests = dataArray.GetUInt(13);
                table.LeftTableId = dataArray.GetString(21);
                table.RightTableId = dataArray.GetString(22);
                table.AssignmentTypeId = dataArray.GetString(23);

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
            var args = new object[3] { session.ClientId, session.PosId, tableName };
            return Intersystems.CallClassMethod("cmNT.Tisch", "GetTischIntern", args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public Task<string> GetTableName(Nt.Data.Session session, string tableId)
        {
            var args = new object[3] { session.ClientId, session.PosId, tableId };
            return Intersystems.CallClassMethod("cmNT.Tisch", "GetTischDisplay", args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public Task<string> GetNewSubTableId(Nt.Data.Session session, string tableId)
        {
            var args = new object[2] { session.ClientId, tableId };
            return Intersystems.CallClassMethod("cmNT.Tisch", "SplittTischNeu", args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        public async Task OpenTable(Nt.Data.Session session, string tableId)
        {
            var args = new object[5] { session.ClientId, session.PosId, session.WaiterId, tableId, "0" };
            var dbString = await Intersystems.CallClassMethod("cmNT.Tisch", "TischOpen", args).ConfigureAwait(false);
            var dataString = new DataString(dbString);
            var dataArray = new DataArray(dataString.SplitByPipe());

            var error = dataArray.GetString(0).Equals("0");
            if (error)
            {
                var errorCode = dataArray.GetString(1);
                var errorMessage = dataArray.GetString(2);
                var tableName = GetTableName(session, tableId);
                //TODO uncomment switch statement
                //switch (errorCode)
                //{
                //    case "2":
                //        throw new Exception(string.Format(Resources.Dictionary.GetString("Table_NotDefined"), tableName));
                //    case "5":
                //        throw new Exception(string.Format(Resources.Dictionary.GetString("Table_NoOpenPermission"), tableName));
                //    case "6":
                //        throw new Exception(string.Format(Resources.Dictionary.GetString("Tabe_AlreadyOpen"), tableName));
                //    default:
                //        throw new Exception(string.Format(Resources.Dictionary.GetString("Table_OpenError"), tableName));
                //}
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        public Task UnlockTable(Nt.Data.Session session, string tableId)
        {
            var args = new object[4] { session.ClientId, session.PosId, session.WaiterId, tableId };
            return Intersystems.CallClassMethod("cmNT.Tisch", "TischUnlock", args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="sourceTableId"></param>
        /// <param name="targetTableId"></param>
        public async Task SplitStart(Session session, string sourceTableId, string targetTableId)
        {
            var args = new object[4] { session.ClientId, session.PosId, session.SerialNumber, session.WaiterId };
            await Intersystems.CallVoidClassMethod("cmNT.SplittOman", "SetSplittStart", args).ConfigureAwait(false);
            args = new object[6] { session.ClientId, session.PosId, session.SerialNumber, session.WaiterId, sourceTableId, targetTableId };
            await Intersystems.CallVoidClassMethod("cmNT.SplittOman", "SetSplittDaten", args).ConfigureAwait(false);
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
            var args = new object[9] { session.ClientId, session.PosId, session.SerialNumber, session.WaiterId, sourceTableId, targetTableId, orderDataString, orderDataString, quantity };
            var returnValue = await Intersystems.CallClassMethod("cmNT.SplittOman", "SetSplittZeile", args).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public Task SplitDone(Session session)
        {
            var args = new object[4] { session.ClientId, session.PosId, session.SerialNumber, session.WaiterId };
            return Intersystems.CallVoidClassMethod("cmNT.SplittOman", "SetSplittOK", args);
        }
    }
}