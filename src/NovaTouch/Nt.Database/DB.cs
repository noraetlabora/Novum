using InterSystems.Data.IRISClient;
using System;
using System.Data;

namespace Nt.Database
{
    /// <summary>
    /// 
    /// </summary>
    public class DB : IDbConnection
    {
        private static readonly Lazy<DB> lazy = new Lazy<DB>(() => new DB());
        private Api.InterSystems.InterSystems _database;
        private Api.InterSystems.Api _api;

        private DB()
        {
            Resources.Dictionary.Initialize("de-AT");
            Logging.Log.Database.Info("creating InterSystems database / api");
            _database = new Api.InterSystems.InterSystems();
            _api = new Api.InterSystems.Api();
        }

        /// <summary>
        /// Singelton Design Pattern (http://csharpindepth.com/Articles/Singleton#lazy)
        /// </summary>
        /// <value></value>
        public static DB Instance { get { return lazy.Value; } }

        /// <summary>
        /// 
        /// </summary>
        public void CheckConnection()
        {
            //foreach (var xepEventPersister in xepEventPersisters)
            //{
            //    switch (xepEventPersister.GetAdoNetConnection().State)
            //    {
            //        case System.Data.ConnectionState.Closed:
            //            Nt.Logging.Log.Database.Warn("DatabaseService: connection is closed");
            //            xepEventPersister.Close();
            //            xepEventPersister.Connect(connectionString);
            //            break;
            //        case System.Data.ConnectionState.Broken:
            //            Nt.Logging.Log.Database.Warn("DatabaseService: connection is broken");
            //            xepEventPersister.Close();
            //            xepEventPersister.Connect(connectionString);
            //            break;
            //        case System.Data.ConnectionState.Connecting:
            //            Nt.Logging.Log.Database.Info("DatabaseService: connection is connecting");
            //            break;
            //        case System.Data.ConnectionState.Executing:
            //            Nt.Logging.Log.Database.Info("DatabaseService: connection is executing");
            //            break;
            //        case System.Data.ConnectionState.Fetching:
            //            Nt.Logging.Log.Database.Info("DatabaseService: connection is fetching");
            //            break;
            //    }
            //}

            Logging.Log.Database.Info(string.Format("connections active {0}, idle {1}, inUse {2}", IRISPoolManager.ActiveConnectionCount, IRISPoolManager.IdleCount(), IRISPoolManager.InUseCount()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public static Api.IDbApi Api
        {
            get { return Instance._api; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionCount"></param>
        public void Initialize(uint connectionCount = 1)
        {
            _database.Initialize(connectionCount);
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
            _database.Open();
            _api.Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            _database.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string ConnectionString
        {
            get { return _database.ConnectionString; }
            set { _database.ConnectionString = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public int ConnectionTimeout { get => _database.ConnectionTimeout; }

        /// <summary>
        /// 
        /// </summary>
        public string Database { get => _database.Database; }

        /// <summary>
        /// 
        /// </summary>
        public ConnectionState State { get => _database.State; }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _database.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbTransaction BeginTransaction()
        {
            return _database.BeginTransaction();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return _database.BeginTransaction(isolationLevel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseName"></param>
        public void ChangeDatabase(string databaseName)
        {
            _database.ChangeDatabase(databaseName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbCommand CreateCommand()
        {
            return _database.CreateCommand();
        }

        #endregion

    }
}