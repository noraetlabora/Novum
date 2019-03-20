using System;
using System.Collections.Generic;

namespace Novum.Logic
{
    /// <summary>
    /// 
    /// </summary>
    public class CancellationReason
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public static List<Novum.Data.CancellationResason> GetCancellationReasons(string department)
        {
            return Novum.Database.DB.Api.Misc.GetCancellationReason(department);
        }
    }
}