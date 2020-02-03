using InterSystems.Data.IRISClient;
using System;
using System.Data;

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

        internal static DataTable GetDataTable(string sql)
        {
            var dataTable = new DataTable();
            var stackTrace = new System.Diagnostics.StackTrace();
            var traceIdCaller = (uint)DateTime.Now.Ticks.GetHashCode() + "|" + stackTrace.GetFrame(1).GetMethod().Name;
            Logging.Log.Database.Debug(traceIdCaller + "|SQL|" + sql);

            //Todo
            System.Diagnostics.Debug.WriteLine("GetDataTable ThreadId: " + System.Threading.Thread.CurrentThread.ManagedThreadId);

            try
            {
                //System.Diagnostics.Debug.WriteLine("GetDataTable is locked?" + System.Threading.Monitor.TryEnter(DB.Connection));
                System.Threading.Monitor.Enter(DB.Connection);
                var dataAdapter = new IRISDataAdapter(sql, DB.Connection);
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                Logging.Log.Database.Debug(traceIdCaller + "|SQLRowCount|" + dataTable.Rows.Count);
            }
            catch (Exception ex)
            {
                if (DB.Connection.State == ConnectionState.Closed ||
                    DB.Connection.State == ConnectionState.Broken)
                {
                    Logging.Log.Database.Error(ex, traceIdCaller + "|SQL|no connection to database");
                    throw new Exception(Resources.Dictionary.GetString("DB_NoConnection"));
                }
                Logging.Log.Database.Error(ex, traceIdCaller + "|SQL|" + sql);
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

        internal static string CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11 };
            return CallClassMethod(className, methodName, args);
        }
        internal static string CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12 };
            return CallClassMethod(className, methodName, args);
        }

        internal static string CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13 };
            return CallClassMethod(className, methodName, args);
        }
        internal static string CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14 };
            return CallClassMethod(className, methodName, args);
        }

        internal static string CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15 };
            return CallClassMethod(className, methodName, args);
        }
        internal static string CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16 };
            return CallClassMethod(className, methodName, args);
        }

        internal static string CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, object arg17)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17 };
            return CallClassMethod(className, methodName, args);
        }

        internal static string CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, object arg17, object arg18)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18 };
            return CallClassMethod(className, methodName, args);
        }

        internal static string CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, object arg17, object arg18, object arg19)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19 };
            return CallClassMethod(className, methodName, args);
        }

        internal static string CallClassMethod(string className, string methodName, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15, object arg16, object arg17, object arg18, object arg19, object arg20)
        {
            var args = new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19, arg20 };
            return CallClassMethod(className, methodName, args);
        }

        private static string CallClassMethod(string className, string methodName, object[] args)
        {
            var stackTrace = new System.Diagnostics.StackTrace();
            var traceIdCaller = (uint)DateTime.Now.Ticks.GetHashCode() + "|" + stackTrace.GetFrame(2).GetMethod().Name;
            var classMethod = string.Format("##class({0}).{1}({2})", className, methodName, string.Join(",", args));
            Logging.Log.Database.Debug(traceIdCaller + "|ClassMethod|" + classMethod);

            //Todo
            System.Diagnostics.Debug.WriteLine("CallClassMethod ThreadId: " + System.Threading.Thread.CurrentThread.ManagedThreadId);

            try
            {
                //System.Diagnostics.Debug.WriteLine("CallClassMethod is locked?" + System.Threading.Monitor.TryEnter(DB.Xep));
                System.Threading.Monitor.Enter(DB.Xep);
                Object returnValue = DB.Xep.CallClassMethod(className, methodName, args);
                Logging.Log.Database.Debug(traceIdCaller + "|ClassMethodReturnValue|" + returnValue.ToString());
                return returnValue.ToString();
            }
            catch (Exception ex)
            {
                if (DB.Connection.State == ConnectionState.Closed ||
                    DB.Connection.State == ConnectionState.Broken)
                {
                    Logging.Log.Database.Error(ex, traceIdCaller + "|ClassMethod|no connection to database");
                    throw new Exception(Resources.Dictionary.GetString("DB_NoConnection"));
                }

                Logging.Log.Database.Error(ex, traceIdCaller + "|ClassMethod|" + classMethod);
                throw ex;
            }
            finally
            {
                System.Threading.Monitor.Exit(DB.Xep);
            }
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
            var traceIdCaller = (uint)DateTime.Now.Ticks.GetHashCode() + "|" + stackTrace.GetFrame(2).GetMethod().Name;
            var classMethod = string.Format("##class({0}).{1}({2})", className, methodName, string.Join(",", args));
            Logging.Log.Database.Debug(traceIdCaller + "|VoidClassMethod|" + classMethod);

            //Todo
            System.Diagnostics.Debug.WriteLine("CallVoidClassMethod ThreadId: " + System.Threading.Thread.CurrentThread.ManagedThreadId);

            try
            {
                //System.Diagnostics.Debug.WriteLine("CallVoidClassMethod is locked?" + System.Threading.Monitor.TryEnter(DB.Xep));
                System.Threading.Monitor.Enter(DB.Xep);
                DB.Xep.CallVoidClassMethod(className, methodName, args);
                Logging.Log.Database.Debug(traceIdCaller + "|VoidClassMethod|success");
            }
            catch (Exception ex)
            {
                if (DB.Connection.State == ConnectionState.Closed ||
                    DB.Connection.State == ConnectionState.Broken)
                {
                    Logging.Log.Database.Error(ex, traceIdCaller + "|VoidClassMethod|no connection to database");
                    throw new Exception(Resources.Dictionary.GetString("DB_NoConnection"));
                }
                Logging.Log.Database.Error(ex, traceIdCaller + "|VoidClassMethod|" + classMethod);
                throw ex;
            }
            finally
            {
                System.Threading.Monitor.Exit(DB.Xep);
            }
        }

        #endregion
    }
}