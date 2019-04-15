using System;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

namespace Novum.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Logger for all Json Data
        /// </summary>
        /// <returns></returns>
        public static Logger Json = LogManager.GetLogger("JsonLog");

        /// <summary>
        /// Logger for Database
        /// </summary>
        /// <returns></returns>
        public static Logger Database = LogManager.GetLogger("DatabaseLog");

        /// <summary>
        /// Logger of the Server
        /// </summary>
        /// <returns></returns>
        public static Logger Server = LogManager.GetLogger("ServerLog");
    }
}