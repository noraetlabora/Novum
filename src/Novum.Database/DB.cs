using System;
using System.Data;
using InterSystems.Data.CacheClient;

namespace Novum.Database
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
            Log.Database.Info("creating Intersystems Caché ADO connection");
            dbConnection = new CacheADOConnection();
            Log.Database.Info("creating Intersystems Caché API");
            api = new Cache.API.CacheApi();
        }

        private static CacheADOConnection dbConnection;
        private static Cache.API.CacheApi api;

        //public DB(string host, string port, string nsp, string user, string password)
        //{
        //    dbConnection = new CacheADOConnection(host, port, nsp, user, password);
        //}

        internal static CacheADOConnection CacheConnection
        {
            get { return dbConnection; }
        }

        public static IDbConnection Connection
        {
            get { return dbConnection; }
        }

        public static API.IDbApi Api
        {
            get { return api; }
        }

        /*******************************************************/
        /******************** IDbConnection ********************/
        /*******************************************************/
        #region IDbConnection
        public void Open()
        {
            try
            {
                Log.Database.Info("opening database connection");
                dbConnection.Open();
                Log.Database.Info("database connection is open");
            }
            catch (Exception ex)
            {
                Log.Database.Fatal(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        public void Close()
        {
            try
            {
                Log.Database.Info("closing database connection");
                dbConnection.Close();
                Log.Database.Info("database connection is closed");
            }
            catch (Exception ex)
            {
                Log.Database.Error(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        public IDbTransaction BeginTransaction()
        {
            return dbConnection.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel isolevel)
        {
            return dbConnection.BeginTransaction(isolevel);
        }

        public void ChangeDatabase(string value)
        {
            dbConnection.ChangeDatabase(value);
        }

        public IDbCommand CreateCommand()
        {
            return dbConnection.CreateCommand();
        }

        public string ConnectionString
        {
            get { return dbConnection.ConnectionString; }
            set
            {
                Log.Database.Debug("setting connection string to: " + value);
                dbConnection.ConnectionString = value;
            }
        }

        public int ConnectionTimeout
        {
            get { return dbConnection.ConnectionTimeout; }
        }

        public string Database
        {
            get { return dbConnection.Database; }
        }

        public ConnectionState State
        {
            get { return dbConnection.State; }
        }

        public void Dispose()
        {
            Log.Database.Info("disposing database connection");
        }

        #endregion


    }
}