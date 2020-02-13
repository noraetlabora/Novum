using InterSystems.Data.IRISClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Nt.Database.Api.InterSystems
{

    internal class Interaction
    {
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

        internal static async Task<DataTable> GetDataTable(string sql)
        {
            var startTicks = DateTime.Now.Ticks;

            var dataTable = new DataTable();
            var stackTrace = new System.Diagnostics.StackTrace();
            var traceIdCaller = (uint)DateTime.Now.Ticks.GetHashCode() + "|" + stackTrace.GetFrame(1).GetMethod().Name;
            Logging.Log.Database.Debug(traceIdCaller + "|SQL|" + sql);

            try
            {
                var adoConnection = (IRISADOConnection)DB.XepEventPersister.GetAdoNetConnection();
                var dataAdapter = new IRISDataAdapter(sql, adoConnection);
                dataTable = new DataTable();
                await Task.Run(() => dataAdapter.Fill(dataTable));
                Logging.Log.Database.Debug(traceIdCaller + "|SQLRowCount|" + dataTable.Rows.Count);
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Error(ex, traceIdCaller + "|SQL|" + sql);
                throw ex;
            }

            return dataTable;
        }

        #region CallClassMethod
        internal static async Task<string> CallClassMethod(string className, string methodName)
        {
            var args = new string[] { };
            return await CallClassMethod(className, methodName, args);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1)
        {
            var args = new object[] { arg1 };
            return await CallClassMethod(className, methodName, args);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2)
        {
            var args = new object[] { arg1, arg2 };
            return await CallClassMethod(className, methodName, args);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3)
        {
            var args = new object[] { arg1, arg2, arg3 };
            return await CallClassMethod(className, methodName, args);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4)
        {
            var args = new object[] { arg1, arg2, arg3, arg4 };
            return await CallClassMethod(className, methodName, args);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5 };
            return await CallClassMethod(className, methodName, args);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6 };
            return await CallClassMethod(className, methodName, args);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 };
            return await CallClassMethod(className, methodName, args);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 };
            return await CallClassMethod(className, methodName, args);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 };
            return await CallClassMethod(className, methodName, args);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 };
            return await CallClassMethod(className, methodName, args);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11 };
            return await CallClassMethod(className, methodName, args);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12 };
            return await CallClassMethod(className, methodName, args);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13 };
            return await CallClassMethod(className, methodName, args);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14 };
            return await CallClassMethod(className, methodName, args);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15 };
            return await CallClassMethod(className, methodName, args);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16 };
            return await CallClassMethod(className, methodName, args);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, object arg17)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17 };
            return await CallClassMethod(className, methodName, args);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, object arg17, object arg18)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18 };
            return await CallClassMethod(className, methodName, args);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, object arg17, object arg18, object arg19)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19 };
            return await CallClassMethod(className, methodName, args);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, object arg17, object arg18, object arg19, object arg20)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19, arg20 };
            return await CallClassMethod(className, methodName, args);
        }

        private static async Task<string> CallClassMethod(string className, string methodName, object[] args)
        {
            var stackTrace = new System.Diagnostics.StackTrace();
            var traceIdCaller = (uint)DateTime.Now.Ticks.GetHashCode() + "|" + stackTrace.GetFrame(2).GetMethod().Name;
            var classMethod = string.Format("##class({0}).{1}({2})", className, methodName, string.Join(",", args));
            Logging.Log.Database.Debug(traceIdCaller + "|ClassMethod|" + classMethod);

            try
            {
                Object returnValue = await Task.Run(()=> DB.XepEventPersister.CallClassMethod(className, methodName, args));
                Logging.Log.Database.Debug(traceIdCaller + "|ClassMethodReturnValue|" + returnValue.ToString());
                return returnValue.ToString();
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Error(ex, traceIdCaller + "|ClassMethod|" + classMethod);
                throw ex;
            }
        }

        #endregion

        #region CallVoidClassMethod

        internal static async Task CallVoidClassMethod(string className, string methodName)
        {
            var args = new string[] { };
            await CallVoidClassMethod(className, methodName, args);
        }

        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1)
        {
            var args = new object[] { arg1 };
            await CallVoidClassMethod(className, methodName, args);
        }
        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2)
        {
            var args = new object[] { arg1, arg2 };
            await CallVoidClassMethod(className, methodName, args);
        }

        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3)
        {
            var args = new object[] { arg1, arg2, arg3 };
            await CallVoidClassMethod(className, methodName, args);
        }

        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4)
        {
            var args = new object[] { arg1, arg2, arg3, arg4 };
            await CallVoidClassMethod(className, methodName, args);
        }
        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5 };
            await CallVoidClassMethod(className, methodName, args);
        }
        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6 };
            await CallVoidClassMethod(className, methodName, args);
        }
        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 };
            await CallVoidClassMethod(className, methodName, args);
        }
        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 };
            await CallVoidClassMethod(className, methodName, args);
        }
        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 };
            await CallVoidClassMethod(className, methodName, args);
        }

        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 };
            await CallVoidClassMethod(className, methodName, args);
        }

        private static async Task CallVoidClassMethod(string className, string methodName, object[] args)
        {
            var stackTrace = new System.Diagnostics.StackTrace();
            var traceIdCaller = (uint)DateTime.Now.Ticks.GetHashCode() + "|" + stackTrace.GetFrame(2).GetMethod().Name;
            var classMethod = string.Format("##class({0}).{1}({2})", className, methodName, string.Join(",", args));
            Logging.Log.Database.Debug(traceIdCaller + "|VoidClassMethod|" + classMethod);

            try
            {
                await Task.Run(() => DB.XepEventPersister.CallVoidClassMethod(className, methodName, args));
                Logging.Log.Database.Debug(traceIdCaller + "|VoidClassMethod|success");
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Error(ex, traceIdCaller + "|VoidClassMethod|" + classMethod);
                throw ex;
            }
        }

        #endregion
    }
}