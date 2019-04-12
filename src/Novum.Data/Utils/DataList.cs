using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Novum.Data.Utils
{
    /// <summary>
    /// DataList is a List of strings which can be accessed.
    /// </summary>
    public class DataList : IEnumerable<string>
    {
        #region private fields
        private static NumberStyles style = NumberStyles.AllowDecimalPoint;
        private static CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
        private List<string> list;

        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringArray"></param>
        public DataList(string[] stringArray)
        {
            if (stringArray == null)
            {
                list = new List<string>();
            }
            else
            {
                list = new List<string>(stringArray);
            }
        }

        #endregion

        #region Enumerator
        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
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
            if (index < 0 && index >= list.Count)
                return 0;
            uint value = 0;
            if (!uint.TryParse(list[index], out value))
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
            if (index < 0 && index >= list.Count)
                return 0;
            int value = 0;
            if (!int.TryParse(list[index], out value))
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
            if (index < 0 && index >= list.Count)
                return decimal.Zero;
            decimal value = decimal.Zero;
            if (!decimal.TryParse(list[index], style, culture, out value))
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
            if (index < 0 && index >= list.Count)
                return false;
            bool value = false;
            if (!bool.TryParse(list[index], out value))
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
            if (index < 0 && index >= list.Count)
                return string.Empty;
            if (string.IsNullOrEmpty(list[index]))
                return string.Empty;
            return list[index];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DateTime GetDateTime(int index)
        {
            if (index < 0 && index >= list.Count)
                return new DateTime(1999, 12, 31, 23, 59, 59, 999);
            DateTime value;
            if (!DateTime.TryParse(list[index], out value))
                return new DateTime(1999, 12, 31, 23, 59, 59, 999);
            return value;
        }

        #endregion
    }
}
