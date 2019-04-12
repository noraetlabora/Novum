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