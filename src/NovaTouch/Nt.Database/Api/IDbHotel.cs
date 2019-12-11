using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.Database.Api
{
 
    /// <summary>
    /// 
    /// </summary>
    public interface IDbHotel
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="partnerId"></param>
        /// <param name="roomNumber"></param>
        Nt.Data.Room GetRoom(Nt.Data.Session session, string partnerId, string roomNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Dictionary<string, Nt.Data.Room> GetRooms(Nt.Data.Session session, string partnerId);
    }
}
