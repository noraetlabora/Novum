using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nt.Database.Api.InterSystems
{
    /// <summary>
    /// 
    /// </summary>
    public class Hotel : IDbHotel
    {
        #region constructor

        /// <summary>
        /// 
        /// </summary>
        public Hotel()
        {

        }

        #endregion

        #region public methods      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="partnerId"></param>
        /// <param name="roomNumber"></param>
        public async Task<Nt.Data.Room> GetRoom(Nt.Data.Session session, string partnerId, string roomNumber)
        {
            Nt.Data.Room room = null;
            var dbString = await Intersystems.Instance.CallClassMethod("cmNT.AbrOmanHotelIFC", "GetZimmerDetail", session.ClientId, session.PosId, session.WaiterId, partnerId, roomNumber);
            var dataString = new DataString(dbString);
            var roomsString = dataString.SplitByChar96();

            foreach (var roomString in roomsString)
            {
                room = new Data.Room();
                var roomDataString = new DataString(roomString);
                var roomDataList = new DataList(roomDataString.SplitByDoublePipes());
                room.Id = roomDataList.GetString(0);
                room.Name = roomDataList.GetString(2);
                room.BookingNumber = roomDataList.GetString(3);
            }

            return room;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, Nt.Data.Room>> GetRooms(Nt.Data.Session session, string partnerId)
        {
            var rooms = new Dictionary<string, Nt.Data.Room>();
            var dbString = await Intersystems.Instance.CallClassMethod("cmNT.AbrOmanHotelIFC", "GetZimmerliste", session.ClientId, session.PosId, session.WaiterId, partnerId, "", "");
            var dataString = new DataString(dbString);
            var dataList = new DataList(dataString.SplitByChar96());

            return rooms;
        }


        #endregion
    }
}
