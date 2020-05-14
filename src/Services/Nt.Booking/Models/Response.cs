using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.Booking.Models
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Response
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract string ToJson();
    }
}
