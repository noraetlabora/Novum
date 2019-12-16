using NLog;

namespace Nt.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Logger for all Communication Data between server and client
        /// </summary>
        /// <returns></returns>
        public static Logger Communication = LogManager.GetLogger("CommunicationLog");

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

        /// <summary>
        /// Logger of the Service
        /// </summary>
        /// <returns></returns>
        public static Logger Service = LogManager.GetLogger("ServiceLog");
    }
}