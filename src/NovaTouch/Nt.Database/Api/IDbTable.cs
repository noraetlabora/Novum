using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<Dictionary<string, Nt.Data.Table>> GetTables(Nt.Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<string> GetTableId(Nt.Data.Session session, string tableName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        Task<string> GetTableName(Nt.Data.Session session, string tableId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        Task<string> GetNewSubTableId(Nt.Data.Session session, string tableId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        Task OpenTable(Nt.Data.Session session, string tableId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        Task UnlockTable(Nt.Data.Session session, string tableId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="sourceTableId"></param>
        /// <param name="targetTableId"></param>
        Task SplitStart(Nt.Data.Session session, string sourceTableId, string targetTableId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="sourceTableId"></param>
        /// <param name="targetTableId"></param>
        /// <param name="order"></param>
        /// <param name="quantity"></param>
        Task SplitOrder(Nt.Data.Session session, string sourceTableId, string targetTableId, Nt.Data.Order order, decimal quantity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        Task SplitDone(Nt.Data.Session session);
    }
}