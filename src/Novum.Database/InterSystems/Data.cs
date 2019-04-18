using System;
using System.Data;
using System.Reflection;
using InterSystems.Data.IRISClient;

namespace Novum.Database.InterSystems
{
    /// <summary>
    /// 
    /// </summary>
    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public const string DoublePipes = "||";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public const char Char96 = (char)96;

        internal static string SqlToday
        {
            get { return DateTime.Now.ToString("yyyy-MM-dd"); }
        }

        /// <summary>
        /// ClientId 
        /// </summary>
        /// <value></value>
        public static string ClientId { get; set; }
    }
}