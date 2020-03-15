using System;
using System.Globalization;

namespace Nt.Database
{
    /// <summary>
    /// DataList is a List of strings which can be accessed.
    /// </summary>
    internal class DataArray
    {
        #region private fields
        private static NumberStyles _style = NumberStyles.AllowDecimalPoint;
        private static CultureInfo _culture = CultureInfo.CreateSpecificCulture("en-US");
        private string[] _stringArray;

        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringArray"></param>
        public DataArray(string[] stringArray)
        {
            if (stringArray == null)
            {
                _stringArray = Array.Empty<string>();
            }
            else
            {
                _stringArray = stringArray;
            }
        }

        #endregion

        #region public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public uint GetUInt(int index)
        {
            if (index < 0 || index >= _stringArray.Length)
                return 0;
            uint value = 0;
            if (!uint.TryParse(_stringArray[index], out value))
                return 0;
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetInt(int index)
        {
            if (index < 0 || index >= _stringArray.Length)
                return 0;
            int value = 0;
            if (!int.TryParse(_stringArray[index], out value))
                return 0;
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public decimal GetDecimal(int index)
        {
            if (index < 0 || index >= _stringArray.Length)
                return decimal.Zero;
            decimal value = decimal.Zero;
            if (!decimal.TryParse(_stringArray[index], _style, _culture, out value))
                return decimal.Zero;
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool GetBool(int index)
        {
            if (index < 0 || index >= _stringArray.Length)
                return false;
            bool value = false;
            if (!bool.TryParse(_stringArray[index], out value))
                return false;
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetString(int index)
        {
            if (index < 0 || index >= _stringArray.Length)
                return string.Empty;
            if (string.IsNullOrEmpty(_stringArray[index]))
                return string.Empty;
            return _stringArray[index];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DateTime GetDateTime(int index)
        {
            if (index < 0 || index >= _stringArray.Length)
                return new DateTime(1999, 12, 31, 23, 59, 59, 999);
            DateTime value;
            if (!DateTime.TryParse(_stringArray[index], out value))
                return new DateTime(1999, 12, 31, 23, 59, 59, 999);
            return value;
        }

        #endregion
    }
}
