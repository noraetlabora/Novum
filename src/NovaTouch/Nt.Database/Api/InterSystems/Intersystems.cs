using InterSystems.Data.IRISClient;
using InterSystems.XEP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Nt.Database.Api.InterSystems
{
    /// <summary>
    /// 
    /// </summary>
    internal class InterSystems : IDbConnection
    {
        private static InterSystems _instance = null;
        private string _connectionString;
        private static EventPersister[] _xepEventPersisters;

        internal InterSystems()
        {
            Console.WriteLine("Intersystems constructor");
        }

        internal static InterSystems Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new InterSystems();
                return _instance;
            }
        }

        internal void Initialize(uint connectionCount = 1)
        {
            if (connectionCount < 1)
            {
                Logging.Log.Database.Error("database needs at least one connection");
                throw new Exception("database needs at least one connection");
            }

            Logging.Log.Database.Info("creating InterSystems XepEventPersister");
            _xepEventPersisters = new EventPersister[connectionCount];
            for (int i = 0; i < connectionCount; i++)
            {
                _xepEventPersisters[i] = PersisterFactory.CreatePersister();
            }
        }

        internal void CheckConnection()
        {
            //foreach (var xepEventPersister in _xepEventPersisters)
            //{
            //    switch (xepEventPersister.GetAdoNetConnection().State)
            //    {
            //        case System.Data.ConnectionState.Closed:
            //            Nt.Logging.Log.Database.Warn("DatabaseService: connection is closed");
            //            Console.WriteLine("DatabaseService: connection is closed");
            //            xepEventPersister.Close();
            //            xepEventPersister.Connect(_connectionString);
            //            Console.WriteLine(" reconnecting xepEventPersister, state = " + xepEventPersister.GetAdoNetConnection().State);
            //            break;
            //        case System.Data.ConnectionState.Broken:
            //            Nt.Logging.Log.Database.Warn("DatabaseService: connection is broken");
            //            xepEventPersister.Close();
            //            xepEventPersister.Connect(_connectionString);
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
            var index = -1;
            var xepEventPersisterFound = false;
            var dataTable = new DataTable();
            var ticks = DateTime.Now.Ticks;

            if (Logging.Log.Database.IsDebugEnabled)
                Logging.Log.Database.Debug(ticks + "|" + memberName + "|SQL|" + sql);

            try
            {
                while (!xepEventPersisterFound)
                {
                    for (index = 0; index < _xepEventPersisters.Length; index++)
                    {
                        if (System.Threading.Monitor.TryEnter(_xepEventPersisters[index]))
                        {
                            xepEventPersisterFound = true;
                            break;
                        }
                    }
                    if (!xepEventPersisterFound)
                        Logging.Log.Database.Info("no free xepEventPersister available - retrying");
                }

                var adoConnection = (IRISADOConnection)_xepEventPersisters[index].GetAdoNetConnection();
                var dataAdapter = new IRISDataAdapter(sql, adoConnection);
                dataTable = new DataTable();
                await Task.Run(() => dataAdapter.Fill(dataTable)).ConfigureAwait(false);
                adoConnection.Dispose();
                dataAdapter.Dispose();
                
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Error(ex, ticks + "|" + memberName + "|SQL|" + sql);
                throw ex;
            }
            finally
            {
                System.Threading.Monitor.Exit(_xepEventPersisters[index]);
            }

            if (Logging.Log.Database.IsDebugEnabled)
                Logging.Log.Database.Debug(ticks + "|" + memberName + "|SQLRowCount|" + dataTable.Rows.Count);

            return dataTable;
        }

        #endregion

        #region CallClassMethod
        internal static Task<string> CallClassMethod(string className, string methodName, [CallerMemberName]string memberName = "")
        {
            var args = Array.Empty<object>();
            return CallClassMethod(className, methodName, args, memberName);
        }

        internal static Task<string> CallClassMethod(string className, string methodName, object arg1, [CallerMemberName]string memberName = "")
        {
            var args = new object[1] { arg1 };
            return CallClassMethod(className, methodName, args, memberName);
        }
        internal static Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, [CallerMemberName]string memberName = "")
        {
            var args = new object[2] { arg1, arg2 };
            return CallClassMethod(className, methodName, args, memberName);
        }

        internal static Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, [CallerMemberName]string memberName = "")
        {
            var args = new object[3] { arg1, arg2, arg3 };
            return CallClassMethod(className, methodName, args, memberName);
        }

        internal static Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, [CallerMemberName]string memberName = "")
        {
            var args = new object[4] { arg1, arg2, arg3, arg4 };
            return CallClassMethod(className, methodName, args, memberName);
        }
        internal static Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, [CallerMemberName]string memberName = "")
        {
            var args = new object[5] { arg1, arg2, arg3, arg4, arg5 };
            return CallClassMethod(className, methodName, args, memberName);
        }
        internal static Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, [CallerMemberName]string memberName = "")
        {
            var args = new object[6] { arg1, arg2, arg3, arg4, arg5, arg6 };
            return CallClassMethod(className, methodName, args, memberName);
        }
        internal static Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, [CallerMemberName]string memberName = "")
        {
            var args = new object[7] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 };
            return CallClassMethod(className, methodName, args, memberName);
        }
        internal static Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, [CallerMemberName]string memberName = "")
        {
            var args = new object[8] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 };
            return CallClassMethod(className, methodName, args, memberName);
        }
        internal static Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, [CallerMemberName]string memberName = "")
        {
            var args = new object[9] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 };
            return CallClassMethod(className, methodName, args, memberName);
        }

        internal static Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, [CallerMemberName]string memberName = "")
        {
            var args = new object[10] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 };
            return CallClassMethod(className, methodName, args, memberName);
        }

        internal static Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, [CallerMemberName]string memberName = "")
        {
            var args = new object[11] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11 };
            return CallClassMethod(className, methodName, args, memberName);
        }
        internal static Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, [CallerMemberName]string memberName = "")
        {
            var args = new object[12] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12 };
            return CallClassMethod(className, methodName, args, memberName);
        }

        internal static Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, [CallerMemberName]string memberName = "")
        {
            var args = new object[13] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13 };
            return CallClassMethod(className, methodName, args, memberName);
        }
        internal static Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, [CallerMemberName]string memberName = "")
        {
            var args = new object[14] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14 };
            return CallClassMethod(className, methodName, args, memberName);
        }

        internal static Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, [CallerMemberName]string memberName = "")
        {
            var args = new object[15] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15 };
            return CallClassMethod(className, methodName, args, memberName);
        }
        internal static Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, [CallerMemberName]string memberName = "")
        {
            var args = new object[16] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16 };
            return CallClassMethod(className, methodName, args, memberName);
        }

        internal static Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, object arg17, [CallerMemberName]string memberName = "")
        {
            var args = new object[17] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17 };
            return CallClassMethod(className, methodName, args, memberName);
        }

        internal static Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, object arg17, object arg18, [CallerMemberName]string memberName = "")
        {
            var args = new object[18] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18 };
            return CallClassMethod(className, methodName, args, memberName);
        }

        internal static Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, object arg17, object arg18, object arg19, [CallerMemberName]string memberName = "")
        {
            var args = new object[19] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19 };
            return CallClassMethod(className, methodName, args, memberName);
        }

        internal static Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, object arg17, object arg18, object arg19, object arg20, [CallerMemberName]string memberName = "")
        {
            var args = new object[20] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19, arg20 };
            return CallClassMethod(className, methodName, args, memberName);
        }

        private static async Task<string> CallClassMethod(string className, string methodName, object[] args, string memberName)
        {
            var index = -1;
            var xepEventPersisterFound = false;
            var ticks = DateTime.Now.Ticks;

            if (Logging.Log.Database.IsDebugEnabled)
                Logging.Log.Database.Debug(ticks + "|" + memberName + "|ClassMethod|" + FormatClassMethod(className, methodName, args));
            try
            {
                while (!xepEventPersisterFound)
                {
                    for (index = 0; index < _xepEventPersisters.Length; index++)
                    {
                        if (System.Threading.Monitor.TryEnter(_xepEventPersisters[index]))
                        {
                            xepEventPersisterFound = true;
                            break;
                        }
                    }
                    if (!xepEventPersisterFound)
                        Logging.Log.Database.Info("no free xepEventPersister available - retrying");
                }

                Object returnValue = await Task.Run(() => _xepEventPersisters[index].CallClassMethod(className, methodName, args)).ConfigureAwait(false); ;
                if (Logging.Log.Database.IsDebugEnabled)
                    Logging.Log.Database.Debug(ticks + "|" + memberName + "|ClassMethodResult|" + returnValue.ToString());
                return returnValue.ToString();
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Debug(ticks + "|" + memberName + "|ClassMethod|" + FormatClassMethod(className, methodName, args));
                throw ex;
            }
            finally
            {
                System.Threading.Monitor.Exit(_xepEventPersisters[index]);
            }
        }

        #endregion

        #region CallVoidClassMethod

        internal static Task CallVoidClassMethod(string className, string methodName, [CallerMemberName]string memberName = "")
        {
            var args = Array.Empty<object>();
            return CallVoidClassMethod(className, methodName, args, memberName);
        }

        internal static Task CallVoidClassMethod(string className, string methodName, object arg1, [CallerMemberName]string memberName = "")
        {
            var args = new object[1] { arg1 };
            return CallVoidClassMethod(className, methodName, args, memberName);
        }
        internal static Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, [CallerMemberName]string memberName = "")
        {
            var args = new object[2] { arg1, arg2 };
            return CallVoidClassMethod(className, methodName, args, memberName);
        }

        internal static Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, [CallerMemberName]string memberName = "")
        {
            var args = new object[3] { arg1, arg2, arg3 };
            return CallVoidClassMethod(className, methodName, args, memberName);
        }

        internal static Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, [CallerMemberName]string memberName = "")
        {
            var args = new object[4] { arg1, arg2, arg3, arg4 };
            return CallVoidClassMethod(className, methodName, args, memberName);
        }
        internal static Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, [CallerMemberName]string memberName = "")
        {
            var args = new object[5] { arg1, arg2, arg3, arg4, arg5 };
            return CallVoidClassMethod(className, methodName, args, memberName);
        }
        internal static Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, [CallerMemberName]string memberName = "")
        {
            var args = new object[6] { arg1, arg2, arg3, arg4, arg5, arg6 };
            return CallVoidClassMethod(className, methodName, args, memberName);
        }
        internal static Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, [CallerMemberName]string memberName = "")
        {
            var args = new object[7] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 };
            return CallVoidClassMethod(className, methodName, args, memberName);
        }
        internal static Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, [CallerMemberName]string memberName = "")
        {
            var args = new object[8] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 };
            return CallVoidClassMethod(className, methodName, args, memberName);
        }
        internal static Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, [CallerMemberName]string memberName = "")
        {
            var args = new object[9] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 };
            return CallVoidClassMethod(className, methodName, args, memberName);
        }

        internal static Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, [CallerMemberName]string memberName = "")
        {
            var args = new object[10] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 };
            return CallVoidClassMethod(className, methodName, args, memberName);
        }

        private static async Task CallVoidClassMethod(string className, string methodName, object[] args, string memberName)
        {
            var index = -1;
            var xepEventPersisterFound = false;
            var ticks = DateTime.Now.Ticks;

            if (Logging.Log.Database.IsDebugEnabled)
                Logging.Log.Database.Debug(ticks + "|" + memberName + "|VoidClassMethod|" + FormatClassMethod(className, methodName, args));

            try
            {
                while (!xepEventPersisterFound)
                {
                    for (index = 0; index < _xepEventPersisters.Length; index++)
                    {
                        if (System.Threading.Monitor.TryEnter(_xepEventPersisters[index]))
                        {
                            xepEventPersisterFound = true;
                            break;
                        }
                    }
                    if (!xepEventPersisterFound)
                        Logging.Log.Database.Info("no free xepEventPersister available - retrying");
                }

                await Task.Run(() => _xepEventPersisters[index].CallVoidClassMethod(className, methodName, args)).ConfigureAwait(false);
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
                System.Threading.Monitor.TryEnter(_xepEventPersisters[index]);
            }
        }

        private static string FormatClassMethod(string className, string methodName, object[] args)
        {
            return string.Format("##class({0}).{1}({2})", className, methodName, string.Join(",", args));
        }
        #endregion

        #region IDbConnection

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
        /// <param name="isoloationLevel"></param>
        /// <returns></returns>
        public IDbTransaction BeginTransaction(IsolationLevel isoloationLevel)
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
        public void Close()
        {
            try
            {
                Logging.Log.Database.Info("closing intersystems database connection");

                for (var i = 0; i < _xepEventPersisters.Length; i++)
                {
                    _xepEventPersisters[i].Close();
                }

                Logging.Log.Database.Info("intersystems database connection is closed");
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Error(ex, "could not close intersystems database connection");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbCommand CreateCommand()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Open()
        {
            try
            {
                Logging.Log.Database.Info("opening intersystem database connection");

                for (var i = 0; i < _xepEventPersisters.Length; i++)
                {
                    _xepEventPersisters[i].Connect(_connectionString);
                }

                Logging.Log.Database.Info("intersystem database connection are open");
                Logging.Log.Database.Info("active connection count = " + IRISPoolManager.ActiveConnectionCount);
                Logging.Log.Database.Info("in use connection count = " + IRISPoolManager.InUseCount());
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Fatal(ex, "could not open intersystem database connection");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                Logging.Log.Database.Info("setting connection string to: " + value.Substring(0, 55) + "...");
                _connectionString = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ConnectionTimeout => 0;

        /// <summary>
        /// 
        /// </summary>
        public string Database => "";

        public ConnectionState State => throw new NotImplementedException();

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        public void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Intersystems()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}