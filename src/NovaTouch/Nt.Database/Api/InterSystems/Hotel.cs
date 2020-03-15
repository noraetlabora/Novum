using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nt.Database.Api.Intersystems
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
            var args = new object[5] { session.ClientId, session.PosId, session.WaiterId, partnerId, roomNumber };
            var dbString = await Intersystems.CallClassMethod("cmNT.AbrOmanHotelIFC", "GetZimmerDetail", args).ConfigureAwait(false);
            var dataString = new DataString(dbString);
            var roomsString = dataString.SplitByChar96();

            foreach (var roomString in roomsString)
            {
                room = new Data.Room();
                var roomDataString = new DataString(roomString);
                var roomDataArray = new DataArray(roomDataString.SplitByDoublePipes());
                room.Id = roomDataArray.GetString(0);
                room.Name = roomDataArray.GetString(2);
                room.BookingNumber = roomDataArray.GetString(3);
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
            var args = new object[6] { session.ClientId, session.PosId, session.WaiterId, partnerId, "", "" };
            var dbString = await Intersystems.CallClassMethod("cmNT.AbrOmanHotelIFC", "GetZimmerliste", args).ConfigureAwait(false); ;
            var dataString = new DataString(dbString);
            var dataArray = new DataArray(dataString.SplitByChar96());
            //TODO: make roomlist out of database dataarray
            return rooms;
        }


        #endregion
    }
}
