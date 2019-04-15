using System;
using System.Data;
using System.Reflection;
using InterSystems.Data.IRISClient;

namespace Novum.Database.InterSystems
{

    internal class Interaction
    {
        /// <summary>
        /// returns the current date in single quotes
        /// </summary>
        /// <value>'2019-12-31'</value>
        internal static string SqlToday
        {
            get
            { return "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'"; }
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

        internal static DataTable GetDataTable(string sql)
        {
            var dataTable = new DataTable();
            var stackTrace = new System.Diagnostics.StackTrace();
            var caller = stackTrace.GetFrame(1).GetMethod().Name;
            Logging.Log.Database.Debug(caller + "|SQL|" + sql);

            try
            {
                System.Threading.Monitor.Enter(DB.Connection);
                var dataAdapter = new IRISDataAdapter(sql, DB.Connection);
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                Logging.Log.Database.Debug(caller + "|DataTableRowCount|" + dataTable.Rows.Count);
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Error(ex, caller + "|SQL|" + sql);
                throw ex;
            }
            finally
            {
                System.Threading.Monitor.Exit(DB.Connection);
            }

            return dataTable;
        }

        #region CallClassMethod
        internal static string CallClassMethod(string className, string methodName)
        {
            var args = new string[] { };
            return CallClassMethod(className, methodName, args);
        }

        internal static string CallClassMethod(string className, string methodName, object arg1)
        {
            var args = new object[] { arg1 };
            return CallClassMethod(className, methodName, args);
        }
        internal static string CallClassMethod(string className, string methodName, object arg1, object arg2)
        {
            var args = new object[] { arg1, arg2 };
            return CallClassMethod(className, methodName, args);
        }

        internal static string CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3)
        {
            var args = new object[] { arg1, arg2, arg3 };
            return CallClassMethod(className, methodName, args);
        }

        internal static string CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4)
        {
            var args = new object[] { arg1, arg2, arg3, arg4 };
            return CallClassMethod(className, methodName, args);
        }
        internal static string CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5 };
            return CallClassMethod(className, methodName, args);
        }
        internal static string CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6 };
            return CallClassMethod(className, methodName, args);
        }
        internal static string CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 };
            return CallClassMethod(className, methodName, args);
        }
        internal static string CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 };
            return CallClassMethod(className, methodName, args);
        }
        internal static string CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 };
            return CallClassMethod(className, methodName, args);
        }

        internal static string CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 };
            return CallClassMethod(className, methodName, args);
        }

        private static string CallClassMethod(string className, string methodName, object[] args)
        {
            var returnValue = "";
            var stackTrace = new System.Diagnostics.StackTrace();
            var caller = stackTrace.GetFrame(2).GetMethod().Name;
            var classMethod = string.Format("##class({0}).{1}({2})", className, methodName, string.Join(",", args));
            Logging.Log.Database.Debug(caller + "|ClassMethod|" + classMethod);

            try
            {
                returnValue = (string)DB.Xep.CallClassMethod(className, methodName, args);
                Logging.Log.Database.Debug(caller + "|ClassMethodReturnValueLength|" + returnValue.Length);
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Error(ex, caller + "|ClassMethod|" + classMethod);
                throw ex;
            }

            return returnValue;
        }

        #endregion

        #region CallVoidClassMethod

        internal static void CallVoidClassMethod(string className, string methodName)
        {
            var args = new string[] { };
            CallVoidClassMethod(className, methodName, args);
        }

        internal static void CallVoidClassMethod(string className, string methodName, object arg1)
        {
            var args = new object[] { arg1 };
            CallVoidClassMethod(className, methodName, args);
        }
        internal static void CallVoidClassMethod(string className, string methodName, object arg1, object arg2)
        {
            var args = new object[] { arg1, arg2 };
            CallVoidClassMethod(className, methodName, args);
        }

        internal static void CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3)
        {
            var args = new object[] { arg1, arg2, arg3 };
            CallVoidClassMethod(className, methodName, args);
        }

        internal static void CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4)
        {
            var args = new object[] { arg1, arg2, arg3, arg4 };
            CallVoidClassMethod(className, methodName, args);
        }
        internal static void CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5 };
            CallVoidClassMethod(className, methodName, args);
        }
        internal static void CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6 };
            CallVoidClassMethod(className, methodName, args);
        }
        internal static void CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 };
            CallVoidClassMethod(className, methodName, args);
        }
        internal static void CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 };
            CallVoidClassMethod(className, methodName, args);
        }
        internal static void CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 };
            CallVoidClassMethod(className, methodName, args);
        }

        internal static void CallVoidClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 };
            CallVoidClassMethod(className, methodName, args);
        }

        private static void CallVoidClassMethod(string className, string methodName, object[] args)
        {
            var stackTrace = new System.Diagnostics.StackTrace();
            var caller = stackTrace.GetFrame(2).GetMethod().Name;
            var classMethod = string.Format("##class({0}).{1}({2})", className, methodName, string.Join(",", args));
            Logging.Log.Database.Debug(caller + "|VoidClassMethod|" + classMethod);

            try
            {
                DB.Xep.CallVoidClassMethod(className, methodName, args);
                Logging.Log.Database.Debug(caller + "|VoidClassMethod|success");
            }
            catch (Exception ex)
            {
                Logging.Log.Database.Error(ex, caller + "|VoidClassMethod|" + classMethod);
                throw ex;
            }
        }

        #endregion
    }
}