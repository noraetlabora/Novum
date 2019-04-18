using System.Collections.Generic;

namespace Novum.Database.Api
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
        Dictionary<string, Data.Table> GetTables(Novum.Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        string GetTableId(Novum.Data.Session session, string tableName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        string GetTableName(Novum.Data.Session session, string tableId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        string GetNewSubTableId(Novum.Data.Session session, string tableId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        void OpenTable(Novum.Data.Session session, string tableId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        void UnlockTable(Novum.Data.Session session, string tableId);
    }
}