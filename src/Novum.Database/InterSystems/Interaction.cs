using System;
using System.Data;
using System.Reflection;
using InterSystems.Data.IRISClient;

namespace Novum.Database.InterSystems
{

    internal class Interaction
    {
        internal static string SqlToday
        {
            get
            { return DateTime.Now.ToString("yyyy-MM-dd"); }
        }

        internal static DataTable GetDataTable(string sql)
        {
            var dataTable = new DataTable();
            var stackTrace = new System.Diagnostics.StackTrace();
            var callingMethod = stackTrace.GetFrame(1).GetMethod().Name;
            //log SQL 
            Log.Database.Debug(callingMethod + "|SQL|" + sql);

            try
            {
                System.Threading.Monitor.Enter(DB.Connection);
                var dataAdapter = new IRISDataAdapter(sql, DB.Connection);
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                Log.Database.Debug(callingMethod + "|Datd aTableRowCount|" + dataTable.Rows.Count);
            }
            catch (Exception ex)
            {
                Log.Database.Error(callingMethod + "|SQL|" + sql);
                Log.Database.Error(callingMethod + "|ExceptionMessage|" + ex.Message);
                Log.Database.Error(callingMethod + "|ExceptionStackTrace|" + ex.StackTrace);
                throw ex;
            }
            finally
            {
                System.Threading.Monitor.Exit(DB.Connection);
            }

            return dataTable;
        }

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
            var callingMethod = stackTrace.GetFrame(2).GetMethod().Name;
            var classMethod = string.Format("##class({0}).{1}({2})", className, methodName, string.Join(",", args));
            Log.Database.Debug(callingMethod + "|ClassMethod|" + classMethod);

            try
            {
                returnValue = (string)DB.Xep.CallClassMethod(className, methodName, args);

                Log.Database.Trace(callingMethod + "|ClassMethodReturnValue|" + returnValue);
                Log.Database.Debug(callingMethod + "|ClassMethodReturnValueLength|" + returnValue.Length);
            }
            catch (Exception ex)
            {
                Log.Database.Error(callingMethod + "|ClassMethod|" + classMethod);
                Log.Database.Error(callingMethod + "|ExceptionMessage|" + ex.Message);
                Log.Database.Error(callingMethod + "|ExceptionStackTrace|" + ex.StackTrace);
                throw ex;
            }

            return returnValue;
        }
    }
}