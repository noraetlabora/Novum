using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Os.Server.Logic
{
    /// <summary>
    /// 
    /// </summary>
    public class Fiscal
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="posId"></param>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        public static object GetProvider(string clientId, string posId, string serialNumber)
        {
            return Nt.Database.DB.Api.Fiscal.GetProvider(clientId, posId, serialNumber);
        }
    }
}
