using System;
using System.Data;

namespace Novum.Database
{
    internal static class DataObject
    {
        private static bool IsNull(DataRow dataRow, string column)
        {
            if (string.IsNullOrWhiteSpace(column))
                return true;
            try
            {
                if (dataRow.IsNull(column))
                    return true;
            }
            catch (Exception ex)
            {
                Log.Database.Error("DataRow.IsNull threw Exception" + ex.Message);
                Log.Database.Error(ex.StackTrace);
                Log.Database.Error("dataRow = " + dataRow.ToString());
                Log.Database.Error("column = " + column);
                return true;
            }
            return false;
        }
        public static string GetString(DataRow dataRow, string column)
        {
            if (IsNull(dataRow, column))
                return string.Empty;

            return Convert.ToString(dataRow[column]);
        }

        public static Int32 GetInt(DataRow dataRow, string column)
        {
            if (IsNull(dataRow, column))
                return 0;

            return Convert.ToInt32(dataRow[column]);
        }

        public static UInt32 GetUInt(DataRow dataRow, string column)
        {
            if (IsNull(dataRow, column))
                return 0;

            return Convert.ToUInt32(dataRow[column]);
        }
    }
}