using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Novum.Database.Cache
{
    /// <summary>
    /// CacheList is a List of strings which can be accessed.
    /// </summary>
    internal class CacheList : IEnumerable<string>
    {
        private static NumberStyles style = NumberStyles.AllowDecimalPoint;
        private static CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
        private List<string> list;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringArray"></param>
        public CacheList(string[] stringArray)
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

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public uint UIntAt(int index)
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
        public int IntAt(int index)
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
        public decimal DecimalAt(int index)
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
        public bool BoolAt(int index)
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
        public string StringAt(int index)
        {
            if (index < 0 && index >= list.Count)
                return string.Empty;
            if (string.IsNullOrEmpty(list[index]))
                return string.Empty;
            return list[index];
        }
    }
}
