using InterSystems.Data.IRISClient;
using InterSystems.XEP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

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
            Resources.Dictionary.Initialize("de-AT");
            Logging.Log.Database.Info("creating InterSystems API");
            api = new Api.InterSystems.Api();
        }

        private static string connectionString;
        private static List<IRISConnection> dbConnections;
        private static List<EventPersister> xepEventPersisters;
        private static Api.InterSystems.Api api;

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        internal static IRISConnection Connection
        {
            get
            {
                while (true)
                {
                    foreach (var dbConnection in dbConnections)
                    {
                        if (dbConnection.State == ConnectionState.Open)
                        {
                            return dbConnection;
                        }
                    }
                    Logging.Log.Database.Debug("no free dbConnection available - retrying");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        internal static EventPersister XepEventPersister
        {
            get
            {
                while (true)
                {
                    foreach (var xepEventPersister in xepEventPersisters)
                    {
                        if (xepEventPersister.GetAdoNetConnection().State == ConnectionState.Open)
                        {
                            return xepEventPersister;
                        }
                    }
                    Logging.Log.Database.Debug("no free xepEventPersister available - retrying");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void CheckConnection()
        {
            foreach (var xepEventPersister in xepEventPersisters)
            {
                switch (xepEventPersister.GetAdoNetConnection().State)
                {
                    case System.Data.ConnectionState.Closed:
                        Nt.Logging.Log.Database.Warn("DatabaseService: connection is closed");
                        xepEventPersister.Close();
                        xepEventPersister.Connect(connectionString);
                        break;
                    case System.Data.ConnectionState.Broken:
                        Nt.Logging.Log.Database.Warn("DatabaseService: connection is broken");
                        xepEventPersister.Close();
                        xepEventPersister.Connect(connectionString);
                        break;
                    case System.Data.ConnectionState.Connecting:
                        Nt.Logging.Log.Database.Info("DatabaseService: connection is connecting");
                        break;
                    case System.Data.ConnectionState.Executing:
                        Nt.Logging.Log.Database.Info("DatabaseService: connection is executing");
                        break;
                    case System.Data.ConnectionState.Fetching:
                        Nt.Logging.Log.Database.Info("DatabaseService: connection is fetching");
                        break;
                }
            }

            foreach (var dbConnection in dbConnections)
            {
                switch (dbConnection.State)
                {
                    case System.Data.ConnectionState.Closed:
                        Nt.Logging.Log.Database.Warn("DatabaseService: connection is closed");
                        dbConnection.Close();
                        dbConnection.ConnectionString = connectionString;
                        dbConnection.Open();
                        break;
                    case System.Data.ConnectionState.Broken:
                        Nt.Logging.Log.Database.Warn("DatabaseService: connection is broken");
                        dbConnection.Close();
                        dbConnection.ConnectionString = connectionString;
                        dbConnection.Open();
                        break;
                    case System.Data.ConnectionState.Connecting:
                        Nt.Logging.Log.Database.Info("DatabaseService: connection is connecting");
                        break;
                    case System.Data.ConnectionState.Executing:
                        Nt.Logging.Log.Database.Info("DatabaseService: connection is executing");
                        break;
                    case System.Data.ConnectionState.Fetching:
                        Nt.Logging.Log.Database.Info("DatabaseService: connection is fetching");
                        break;
                }
            }
            Logging.Log.Database.Debug(string.Format("connections active {0}, idle {1}, inUse {2}", IRISPoolManager.ActiveConnectionCount, IRISPoolManager.IdleCount(), IRISPoolManager.InUseCount()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public static Api.IDbApi Api
        {
            get { return api; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionCount"></param>
        public void Initialize(uint connectionCount = 1)
        {
            if (connectionCount < 1)
            {
                Logging.Log.Database.Error("database needs at least one connection");
                throw new Exception("database needs at least one connection");
            }

            Logging.Log.Database.Info("creating InterSystems XepEventPersister");
            xepEventPersisters = new List<EventPersister>();
            for (int i = 0; i < connectionCount; i++)
            {
                xepEventPersisters.Add(PersisterFactory.CreatePersister());
            }

            Logging.Log.Database.Info("creating InterSystems IRISConnection");
            dbConnections = new List<IRISConnection>();
            for (int i = 0; i < connectionCount; i++)
            {
                dbConnections.Add(new IRISConnection());
            }
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
                
                foreach (var xepEventPersister in xepEventPersisters)
                {
                    xepEventPersister.Connect(connectionString);}
                foreach (var dbConnection in dbConnections)
                {
                    dbConnection.ConnectionString = connectionString;
                    dbConnection.Open();
                }

                Logging.Log.Database.Info("database connection are open");
                Logging.Log.Database.Info("active connection count = " + IRISPoolManager.ActiveConnectionCount);
                Logging.Log.Database.Info("in use connection count = " + IRISPoolManager.InUseCount());
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Fatal(ex, "could not open database connection");
                throw ex;
            }
            api.Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        public ConnectionState State
        {
            get { return dbConnections[0].State; }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            try
            {
                Logging.Log.Database.Info("closing database connection");
                foreach (var xepEventPersister in xepEventPersisters)
                {
                    xepEventPersister.Close();
                }
                foreach (var dbConnection in dbConnections)
                {
                    dbConnection.Close();
                }
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
        /// <param name="index"></param>
        /// <returns></returns>
        public IDbTransaction BeginTransaction(int index)
        {
            return dbConnections[index].BeginTransaction();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isolevel"></param>
        /// <returns></returns>
        public IDbTransaction BeginTransaction(int index, IsolationLevel isolevel)
        {
            return dbConnections[index].BeginTransaction(isolevel);
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
            get { return dbConnections[0].ConnectionTimeout; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Database
        {
            get { return dbConnections[0].Database; }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Logging.Log.Database.Info("disposing database connection");
            Close();
            Logging.Log.Database.Info("database connection disposed");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseName"></param>
        public void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbCommand CreateCommand()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}