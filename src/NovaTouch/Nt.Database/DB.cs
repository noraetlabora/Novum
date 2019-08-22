using System;
using System.Data;
using System.Data.Common;
using InterSystems.Data.IRISClient;
using InterSystems.XEP;

namespace Nt.Database
{
    /// <summary>
    /// 
    /// </summary>
    public class DB : IDbConnection
    {
        private static readonly Lazy<DB> lazy = new Lazy<DB>(() => new DB());

        /// <summary>
        /// Singelton Design Pattern (http://csharpindepth.com/Articles/Singleton#lazy)
        /// </summary>
        /// <value></value>
        public static DB Instance { get { return lazy.Value; } }
        private DB()
        {
            Logging.Log.Database.Info("creating InterSystems connection");
            xep = PersisterFactory.CreatePersister();
            Logging.Log.Database.Info("creating InterSystems API");
            api = new InterSystems.Api.InterSystemsApi();
        }

        private static string connectionString;
        private static IRISADOConnection dbConnection;
        private static EventPersister xep;
        private static InterSystems.Api.InterSystemsApi api;

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        internal static IRISADOConnection Connection
        {
            get { return dbConnection; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        internal static EventPersister Xep
        {
            get { return xep; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public static Api.IDbApi Api
        {
            get { return api; }
        }

        /*******************************************************/
        /******************** IDbConnection ********************/
        /*******************************************************/
        #region IDbConnection

        /// <summary>
        /// 
        /// </summary>
        public void Open()
        {
            try
            {
                Logging.Log.Database.Info("opening database connection");
                xep.Connect(connectionString);
                dbConnection = (IRISADOConnection)xep.GetAdoNetConnection();
                Logging.Log.Database.Info("database connection is open");
                api.Initialize();
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Fatal(ex, "could not open database connection");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            try
            {
                Logging.Log.Database.Info("closing database connection");
                xep.Close();
                Logging.Log.Database.Info("database connection is closed");
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Error(ex, "could not close database connection");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbTransaction BeginTransaction()
        {
            return dbConnection.BeginTransaction();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isolevel"></param>
        /// <returns></returns>
        public IDbTransaction BeginTransaction(IsolationLevel isolevel)
        {
            return dbConnection.BeginTransaction(isolevel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void ChangeDatabase(string value)
        {
            dbConnection.ChangeDatabase(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbCommand CreateCommand()
        {
            return dbConnection.CreateCommand();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string ConnectionString
        {
            get { return connectionString; }
            set
            {
                Logging.Log.Database.Info("setting connection string to: " + value.Substring(0, 55) + "...");
                connectionString = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public int ConnectionTimeout
        {
            get { return dbConnection.ConnectionTimeout; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Database
        {
            get { return dbConnection.Database; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public ConnectionState State
        {
            get { return dbConnection.State; }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (State != ConnectionState.Closed)
                Close();
            Logging.Log.Database.Info("disposing database connection");
        }

        /// <summary>
        /// 
        /// </summary>
        public void Ping()
        {
            dbConnection.ping();
        }

        #endregion

    }
}