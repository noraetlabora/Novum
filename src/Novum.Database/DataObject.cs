using System;
using System.Data;
using System.Text;

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
                var message = new StringBuilder();
                message.Append("DataObject.DataRow.IsNull").Append(Environment.NewLine);
                message.Append("dataRow = ").Append(dataRow.ToString()).Append(Environment.NewLine);
                message.Append("column = ").Append(column);
                Logging.Log.Database.Error(ex, message.ToString());
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

        public static bool GetBool(DataRow dataRow, string column)
        {
            if (IsNull(dataRow, column))
                return false;

            return Convert.ToBoolean(dataRow[column]);
        }
    }
}