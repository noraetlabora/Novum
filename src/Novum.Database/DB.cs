using System;
using System.Data;
using InterSystems.Data.IRISClient;
using InterSystems.XEP;
using System.Data.Common;

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
            Log.Database.Info("creating InterSystems connection");
            xep = PersisterFactory.CreatePersister();
            Log.Database.Info("creating InterSystems API");
            api = new InterSystems.Api.InterSystemsApi();
        }

        private static string connectionString;
        private static IRISADOConnection dbConnection;
        private static EventPersister xep;
        private static InterSystems.Api.InterSystemsApi api;

        internal static IRISADOConnection Connection
        {
            get { return dbConnection; }
        }

        internal static EventPersister Xep
        {
            get { return xep; }
        }

        public static Api.IDbApi Api
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
                xep.Connect(connectionString);
                dbConnection = (IRISADOConnection)xep.GetAdoNetConnection();
                Log.Database.Info("database connection is open");
                api.Initialize();
            }
            catch (Exception ex)
            {
                Log.Database.Fatal(ex, "could not open database connection");
            }
        }

        public void Close()
        {
            try
            {
                Log.Database.Info("closing database connection");
                xep.Close();
                Log.Database.Info("database connection is closed");
            }
            catch (Exception ex)
            {
                Log.Database.Error(ex, "could not close database connection");
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
            get { return connectionString; }
            set
            {
                Log.Database.Info("setting connection string to: " + value.Substring(0, 50) + "...");
                connectionString = value;
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
            if (State != ConnectionState.Closed)
                Close();
            Log.Database.Info("disposing database connection");
        }

        #endregion

    }
}