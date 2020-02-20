using InterSystems.Data.IRISClient;
using System;
using System.Data;
using System.Runtime.CompilerServices;
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

        internal static async Task<DataTable> GetDataTable(string sql, [CallerMemberName]string memberName = "")
        {
            var dataTable = new DataTable();
            var ticks = DateTime.Now.Ticks;
            if (Logging.Log.Database.IsDebugEnabled)
                Logging.Log.Database.Debug(ticks + "|" + memberName + "|SQL|" + sql);

            try
            {
                var adoConnection = (IRISADOConnection)DB.XepEventPersister.GetAdoNetConnection();
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

        #region CallClassMethod
        internal static async Task<string> CallClassMethod(string className, string methodName, [CallerMemberName]string memberName = "")
        {
            var args = new string[] { };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1 };
            return await CallClassMethod(className, methodName, args, memberName);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4 };
            return await CallClassMethod(className, methodName, args, memberName);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5 };
            return await CallClassMethod(className, methodName, args, memberName);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6 };
            return await CallClassMethod(className, methodName, args, memberName);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 };
            return await CallClassMethod(className, methodName, args, memberName);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 };
            return await CallClassMethod(className, methodName, args, memberName);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11 };
            return await CallClassMethod(className, methodName, args, memberName);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13 };
            return await CallClassMethod(className, methodName, args, memberName);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15 };
            return await CallClassMethod(className, methodName, args, memberName);
        }
        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, object arg17, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, object arg17, object arg18, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, object arg17, object arg18, object arg19, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        internal static async Task<string> CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, object arg17, object arg18, object arg19, object arg20, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19, arg20 };
            return await CallClassMethod(className, methodName, args, memberName);
        }

        private static async Task<string> CallClassMethod(string className, string methodName, object[] args, string memberName)
        {
            var ticks = DateTime.Now.Ticks;
            if (Logging.Log.Database.IsDebugEnabled)
                Logging.Log.Database.Debug(ticks + "|" + memberName + "|ClassMethod|" + FormatClassMethod(className, methodName, args));

            try
            {
                Object returnValue = await Task.Run(()=> DB.XepEventPersister.CallClassMethod(className, methodName, args));
                if (Logging.Log.Database.IsDebugEnabled)
                    Logging.Log.Database.Debug(ticks + "|" + memberName + "|ClassMethodResult|" + returnValue.ToString());

                return returnValue.ToString();
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Debug(ticks + "|" + memberName + "|ClassMethod|" + FormatClassMethod(className, methodName, args));
                throw ex;
            }
        }

        #endregion

        #region CallVoidClassMethod

        internal static async Task CallVoidClassMethod(string className, string methodName, [CallerMemberName]string memberName = "")
        {
            var args = new string[] { };
            await CallVoidClassMethod(className, methodName, args, memberName);
        }

        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1 };
            await CallVoidClassMethod(className, methodName, args, memberName);
        }
        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2 };
            await CallVoidClassMethod(className, methodName, args, memberName);
        }

        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3 };
            await CallVoidClassMethod(className, methodName, args, memberName);
        }

        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4 };
            await CallVoidClassMethod(className, methodName, args, memberName);
        }
        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5 };
            await CallVoidClassMethod(className, methodName, args, memberName);
        }
        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6 };
            await CallVoidClassMethod(className, methodName, args, memberName);
        }
        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 };
            await CallVoidClassMethod(className, methodName, args, memberName);
        }
        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 };
            await CallVoidClassMethod(className, methodName, args, memberName);
        }
        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 };
            await CallVoidClassMethod(className, methodName, args, memberName);
        }

        internal static async Task CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, [CallerMemberName]string memberName = "")
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 };
            await CallVoidClassMethod(className, methodName, args, memberName);
        }

        private static async Task CallVoidClassMethod(string className, string methodName, object[] args, string memberName)
        {
            var ticks = DateTime.Now.Ticks;
            if (Logging.Log.Database.IsDebugEnabled)
                Logging.Log.Database.Debug(ticks + "|" + memberName + "|VoidClassMethod|" + FormatClassMethod(className, methodName, args));

            try
            {
                await Task.Run(() => DB.XepEventPersister.CallVoidClassMethod(className, methodName, args));
                if (Logging.Log.Database.IsDebugEnabled)
                    Logging.Log.Database.Debug(ticks + "|" + memberName + "|VoidClassMethod|success");
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Error(ex, ticks + "|" + memberName + "|VoidClassMethod|" + FormatClassMethod(className, methodName, args));
                throw ex;
            }
        }

        private static string FormatClassMethod(string className, string methodName, object[] args)
        {
            return string.Format("##class({0}).{1}({2})", className, methodName, string.Join(",", args));
        }

        #endregion
    }
}