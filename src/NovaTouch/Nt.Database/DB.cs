using System.Data;

namespace Nt.Database
{
    /// <summary>
    /// 
    /// </summary>
    public class DB : IDbConnection
    {
        //private static readonly Lazy<DB> lazy = new Lazy<DB>(() => new DB()); 
        /// <summary>
        /// Singelton Design Pattern (http://csharpindepth.com/Articles/Singleton#lazy)
        /// </summary>
        /// <value></value>
        //public static DB Instance { get { return lazy.Value; } }
        public static DB Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DB();
                return _instance;
            }
        }

        private static DB _instance = null;
        private Api.InterSystems.Intersystems _database;

        private DB()
        {
            Resources.Dictionary.Initialize("de-AT");
            Logging.Log.Database.Info("creating InterSystems API");
            _database = Nt.Database.Api.InterSystems.Intersystems.Instance;
            api = new Api.InterSystems.Api();
        }

        private static Api.InterSystems.Api api;

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
            _database.Initialize(connectionCount);
        }

        /// <summary>
        /// 
        /// </summary>
        public void CheckConnection()
        {
            _database.CheckConnection();
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
            api.Initialize();
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
            get => _database.ConnectionString;
            set => _database.ConnectionString = value;
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