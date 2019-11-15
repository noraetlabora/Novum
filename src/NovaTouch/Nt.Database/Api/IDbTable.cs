using System.Collections.Generic;

namespace Nt.Database.Api
{
    /// <summary>
    /// Interface for Table Api
    /// </summary>
    public interface IDbTable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<string, Nt.Data.Table> GetTables(Nt.Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        string GetTableId(Nt.Data.Session session, string tableName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        string GetTableName(Nt.Data.Session session, string tableId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        string GetNewSubTableId(Nt.Data.Session session, string tableId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        void OpenTable(Nt.Data.Session session, string tableId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        void UnlockTable(Nt.Data.Session session, string tableId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="sourceTableId"></param>
        /// <param name="targetTableId"></param>
        void SplitStart(Nt.Data.Session session, string sourceTableId, string targetTableId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="sourceTableId"></param>
        /// <param name="targetTableId"></param>
        /// <param name="order"></param>
        /// <param name="quantity"></param>
        void SplitOrder(Nt.Data.Session session, string sourceTableId, string targetTableId, Nt.Data.Order order, decimal quantity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        void SplitDone(Nt.Data.Session session);
    }
}