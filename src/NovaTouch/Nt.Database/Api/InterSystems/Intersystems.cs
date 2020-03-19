using System;
using System.Collections.Concurrent;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Nt.Database.Api.Intersystems
{

    internal class Intersystems : IDbConnection
    {
        private static ConcurrentQueue<InterSystems.XEP.EventPersister> xepEventPersisters;

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
            xepEventPersisters = new ConcurrentQueue<InterSystems.XEP.EventPersister>();
            for (int i = 0; i < connectionCount; i++)
            {
                EnqueueEventPersister(InterSystems.XEP.PersisterFactory.CreatePersister());
            }
        }

        #region IDbConnection implementation

        public string ConnectionString { get; set; }

        public int ConnectionTimeout => 0;

        public string Database => "";

        public ConnectionState State => throw new NotImplementedException();

        public IDbTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            throw new NotImplementedException();
        }

        public void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            try
            {
                Logging.Log.Database.Info("closing InterSystems database connection");

                foreach (var xepEventPersister in xepEventPersisters)
                {
                    xepEventPersister.Close();
                }

                Logging.Log.Database.Info("database InterSystems connection is closed");
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Error(ex, "could not close InterSystems database connection");
            }
        }

        public IDbCommand CreateCommand()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            try
            {
                Logging.Log.Database.Info("opening InterSystems database connection");

                foreach (var xepEventPersister in xepEventPersisters)
                {
                    xepEventPersister.Connect(ConnectionString);
                }

                Logging.Log.Database.Info("InterSystems database connection are open");
                Logging.Log.Database.Info("active connection count = " + InterSystems.Data.IRISClient.IRISPoolManager.ActiveConnectionCount);
                Logging.Log.Database.Info("in use connection count = " + InterSystems.Data.IRISClient.IRISPoolManager.InUseCount());
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Fatal(ex, "could not open InterSystems database connection");
                throw ex;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region SQL 

        /// <summary>
        /// returns the current date in single quotes
        /// </summary>
        /// <value>'2019-12-31'</value>
        internal static string SqlToday
        {
            get { return "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'"; }
        }

        /// <summary>
        /// returns the argument value in single quotes
        /// </summary>
        /// <param name="value"></param>
        /// <returns>'myValue'</returns>
        internal static string SqlQuote(string value)
        {
            return "'" + value + "'";
        }

        internal static async Task<DataTable> GetDataTable(string sql, [CallerMemberName]string memberName = "")
        {
            var ticks = DateTime.Now.Ticks;
            var dataTable = new DataTable();

            if (Logging.Log.Database.IsDebugEnabled)
                Logging.Log.Database.Debug(ticks + "|" + memberName + "|SQL|" + sql);

            //get XepEventPersister from Queue
            var xepEventPersister = DequeueEventPersister();
            try
            {
                var adoConnection = (InterSystems.Data.IRISClient.IRISADOConnection)xepEventPersister.GetAdoNetConnection();
                using (var dataAdapter = new InterSystems.Data.IRISClient.IRISDataAdapter(sql, adoConnection))
                {
                    await Task.Run(() => dataAdapter.Fill(dataTable)).ConfigureAwait(false);
                }

                if (Logging.Log.Database.IsDebugEnabled)
                    Logging.Log.Database.Debug(ticks + "|" + memberName + "|SQLRowCount|" + dataTable.Rows.Count);
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Error(ex, ticks + "|" + memberName + "|SQL|" + sql);
                throw ex;
            }
            finally
            {
                //return XepEventPersister to Queue
                EnqueueEventPersister(xepEventPersister);
            }

            return dataTable;
        }

        #endregion

        #region CallClassMethod

        internal static async Task<string> CallClassMethod(string className, string methodName, object[] args, [CallerMemberName]string memberName = "")
        {
            var ticks = DateTime.Now.Ticks;
            if (Logging.Log.Database.IsDebugEnabled)
                Logging.Log.Database.Debug(ticks + "|" + memberName + "|ClassMethod|" + FormatClassMethod(className, methodName, args));

            //get XepEventPersister from Queue
            var xepEventPersister = DequeueEventPersister();
            try
            {
                Object returnValue = await Task.Run(() => xepEventPersister.CallClassMethod(className, methodName, args)).ConfigureAwait(false);
                if (Logging.Log.Database.IsDebugEnabled)
                    Logging.Log.Database.Debug(ticks + "|" + memberName + "|ClassMethodResult|" + returnValue.ToString());

                return returnValue.ToString();
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Error(ex, ticks + "|" + memberName + "|ClassMethod|" + FormatClassMethod(className, methodName, args));
                throw ex;
            }
            finally
            {
                //return XepEventPersister to Queue
                EnqueueEventPersister(xepEventPersister);
            }
        }

        #endregion

        #region CallVoidClassMethod

        internal static async Task CallVoidClassMethod(string className, string methodName, object[] args, [CallerMemberName]string memberName = "")
        {
            var ticks = DateTime.Now.Ticks;
            if (Logging.Log.Database.IsDebugEnabled)
                Logging.Log.Database.Debug(ticks + "|" + memberName + "|VoidClassMethod|" + FormatClassMethod(className, methodName, args));

            //get XepEventPersister from Queue
            var xepEventPersister = DequeueEventPersister();
            try
            {
                await Task.Run(() => xepEventPersister.CallVoidClassMethod(className, methodName, args)).ConfigureAwait(false);
                if (Logging.Log.Database.IsDebugEnabled)
                    Logging.Log.Database.Debug(ticks + "|" + memberName + "|VoidClassMethod|success");
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Error(ex, ticks + "|" + memberName + "|VoidClassMethod|" + FormatClassMethod(className, methodName, args));
                throw ex;
            }
            finally
            {
                //return XepEventPersister to Queue
                EnqueueEventPersister(xepEventPersister);
            }
        }

        #endregion

        #region private Methods
        private static InterSystems.XEP.EventPersister DequeueEventPersister()
        {
            InterSystems.XEP.EventPersister xepEventPersister = null;
            while (true)
            {
                if (xepEventPersisters.TryDequeue(out xepEventPersister))
                    return xepEventPersister;

                System.Threading.Thread.Sleep(100);
                Logging.Log.Database.Info("no free xepEventPersister available - retrying");
            }
        }

        private static void EnqueueEventPersister(InterSystems.XEP.EventPersister xepEventPersister)
        {
            xepEventPersisters.Enqueue(xepEventPersister);
        }
        private static string FormatClassMethod(string className, string methodName, object[] args)
        {
            return string.Format("##class({0}).{1}({2})", className, methodName, string.Join(",", args));
        }

        #endregion
    }
}