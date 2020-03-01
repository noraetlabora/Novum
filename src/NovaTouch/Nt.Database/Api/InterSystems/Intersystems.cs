using InterSystems.Data.IRISClient;
using InterSystems.XEP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Nt.Database.Api.InterSystems
{

    internal class InterSystems : IDbConnection
    {
        private static List<EventPersister> xepEventPersisters;

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
                    Logging.Log.Database.Info("no free xepEventPersister available - retrying");
                }
            }
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
                Logging.Log.Database.Info("active connection count = " + IRISPoolManager.ActiveConnectionCount);
                Logging.Log.Database.Info("in use connection count = " + IRISPoolManager.InUseCount());
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

            try
            {
                var adoConnection = (IRISADOConnection)XepEventPersister.GetAdoNetConnection();
                var dataAdapter = new IRISDataAdapter(sql, adoConnection);
                dataTable = new DataTable();
                await Task.Run(() => dataAdapter.Fill(dataTable));
                if (Logging.Log.Database.IsDebugEnabled)
                    Logging.Log.Database.Debug(ticks + "|" + memberName + "|SQLRowCount|" + dataTable.Rows.Count);
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Error(ex, ticks + "|" + memberName + "|SQL|" + sql);
                throw ex;
            }

            return dataTable;
        }

        #endregion

        #region CallClassMethod
        internal static async Task<string> CallClassMethod(string className, string methodName, [CallerMemberName]string memberName = "")
        {
            var args = Array.Empty<object>();
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, [CallerMemberName]string memberName = "")
        {
            var args = new object[1] { arg1 };
            return await CallClassMethod(className, methodName, args, memberName);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, [CallerMemberName]string memberName = "")
        {
            var args = new object[2] { arg1, arg2 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, [CallerMemberName]string memberName = "")
        {
            var args = new object[3] { arg1, arg2, arg3 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, [CallerMemberName]string memberName = "")
        {
            var args = new object[4] { arg1, arg2, arg3, arg4 };
            return await CallClassMethod(className, methodName, args, memberName);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, [CallerMemberName]string memberName = "")
        {
            var args = new object[5] { arg1, arg2, arg3, arg4, arg5 };
            return await CallClassMethod(className, methodName, args, memberName);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, [CallerMemberName]string memberName = "")
        {
            var args = new object[6] { arg1, arg2, arg3, arg4, arg5, arg6 };
            return await CallClassMethod(className, methodName, args, memberName);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, [CallerMemberName]string memberName = "")
        {
            var args = new object[7] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 };
            return await CallClassMethod(className, methodName, args, memberName);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, [CallerMemberName]string memberName = "")
        {
            var args = new object[8] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 };
            return await CallClassMethod(className, methodName, args, memberName);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, [CallerMemberName]string memberName = "")
        {
            var args = new object[9] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, [CallerMemberName]string memberName = "")
        {
            var args = new object[10] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, [CallerMemberName]string memberName = "")
        {
            var args = new object[11] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11 };
            return await CallClassMethod(className, methodName, args, memberName);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, [CallerMemberName]string memberName = "")
        {
            var args = new object[12] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, [CallerMemberName]string memberName = "")
        {
            var args = new object[13] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13 };
            return await CallClassMethod(className, methodName, args, memberName);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, [CallerMemberName]string memberName = "")
        {
            var args = new object[14] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, [CallerMemberName]string memberName = "")
        {
            var args = new object[15] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15 };
            return await CallClassMethod(className, methodName, args, memberName);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, [CallerMemberName]string memberName = "")
        {
            var args = new object[16] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, object arg17, [CallerMemberName]string memberName = "")
        {
            var args = new object[17] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, object arg17, object arg18, [CallerMemberName]string memberName = "")
        {
            var args = new object[18] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, object arg17, object arg18, object arg19, [CallerMemberName]string memberName = "")
        {
            var args = new object[19] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, object arg17, object arg18, object arg19, object arg20, [CallerMemberName]string memberName = "")
        {
            var args = new object[20] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19, arg20 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        private static async Task<string> CallClassMethod(string className, string methodName, object[] args, string memberName)
        {
            var ticks = DateTime.Now.Ticks;
            if (Logging.Log.Database.IsDebugEnabled)
                Logging.Log.Database.Debug(ticks + "|" + memberName + "|ClassMethod|" + FormatClassMethod(className, methodName, args));

            try
            {
                Object returnValue = await Task.Run(() => XepEventPersister.CallClassMethod(className, methodName, args));
                if (Logging.Log.Database.IsDebugEnabled)
                    Logging.Log.Database.Debug(ticks + "|" + memberName + "|ClassMethodResult|" + returnValue.ToString());

                return returnValue.ToString();
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Error(ex, ticks + "|" + memberName + "|ClassMethod|" + FormatClassMethod(className, methodName, args));
                throw ex;
            }
        }

        #endregion

        #region CallVoidClassMethod

        internal static async Task CallVoidClassMethod(string className, string methodName, [CallerMemberName]string memberName = "")
        {
            var args = Array.Empty<object>();
            await CallVoidClassMethod(className, methodName, args, memberName);
        }

        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, [CallerMemberName]string memberName = "")
        {
            var args = new object[1] { arg1 };
            await CallVoidClassMethod(className, methodName, args, memberName);
        }
        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, [CallerMemberName]string memberName = "")
        {
            var args = new object[2] { arg1, arg2 };
            await CallVoidClassMethod(className, methodName, args, memberName);
        }

        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, [CallerMemberName]string memberName = "")
        {
            var args = new object[3] { arg1, arg2, arg3 };
            await CallVoidClassMethod(className, methodName, args, memberName);
        }

        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, [CallerMemberName]string memberName = "")
        {
            var args = new object[4] { arg1, arg2, arg3, arg4 };
            await CallVoidClassMethod(className, methodName, args, memberName);
        }
        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, [CallerMemberName]string memberName = "")
        {
            var args = new object[5] { arg1, arg2, arg3, arg4, arg5 };
            await CallVoidClassMethod(className, methodName, args, memberName);
        }
        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, [CallerMemberName]string memberName = "")
        {
            var args = new object[6] { arg1, arg2, arg3, arg4, arg5, arg6 };
            await CallVoidClassMethod(className, methodName, args, memberName);
        }
        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, [CallerMemberName]string memberName = "")
        {
            var args = new object[7] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 };
            await CallVoidClassMethod(className, methodName, args, memberName);
        }
        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, [CallerMemberName]string memberName = "")
        {
            var args = new object[8] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 };
            await CallVoidClassMethod(className, methodName, args, memberName);
        }
        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, [CallerMemberName]string memberName = "")
        {
            var args = new object[9] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 };
            await CallVoidClassMethod(className, methodName, args, memberName);
        }

        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, [CallerMemberName]string memberName = "")
        {
            var args = new object[10] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 };
            await CallVoidClassMethod(className, methodName, args, memberName);
        }

        private static async Task CallVoidClassMethod(string className, string methodName, object[] args, string memberName)
        {
            var ticks = DateTime.Now.Ticks;
            if (Logging.Log.Database.IsDebugEnabled)
                Logging.Log.Database.Debug(ticks + "|" + memberName + "|VoidClassMethod|" + FormatClassMethod(className, methodName, args));

            try
            {
                await Task.Run(() => XepEventPersister.CallVoidClassMethod(className, methodName, args));
                if (Logging.Log.Database.IsDebugEnabled)
                    Logging.Log.Database.Debug(ticks + "|" + memberName + "|VoidClassMethod|success");
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Error(ex, ticks + "|" + memberName + "|VoidClassMethod|" + FormatClassMethod(className, methodName, args));
                throw ex;
            }
        }

        #endregion

        private static string FormatClassMethod(string className, string methodName, object[] args)
        {
            return string.Format("##class({0}).{1}({2})", className, methodName, string.Join(",", args));
        }
    }
}