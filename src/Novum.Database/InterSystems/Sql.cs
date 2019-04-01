using System;

namespace Novum.Database.InterSystems
{
    /// <summary>
    /// 
    /// </summary>
    internal class Sql
    {
        public static string Today
        {
            get { return DateTime.Now.ToString("yyyy-MM-dd"); }
        }
    }
}