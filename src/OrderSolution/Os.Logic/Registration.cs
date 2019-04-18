using System;
using System.Collections.Generic;

namespace Os.Logic
{
    /// <summary>
    /// .
    /// </summary>
    public class Registration
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="clientData"></param>
        /// <returns></returns>
        public static Os.Data.POSInfo RegisterClient(Nt.Data.Session session, Os.Data.ClientInfo clientData)
        {
            var posId = Nt.Database.DB.Api.Pos.GetPosId(clientData.Id);
            if (string.IsNullOrEmpty(posId))
                throw new Exception(string.Format("client {0} not valid", clientData.Id));
            var serviceAreaId = Nt.Database.DB.Api.Pos.GetServiceAreaId(posId);

            var posInfo = new Os.Data.POSInfo();
            posInfo.RestaurantName = Nt.Database.DB.Api.Pos.GetServiceAreaName(serviceAreaId);
            posInfo.ClientName = "Orderman";
            posInfo.UtcTime = Nt.Data.Utils.Unix.TimestampNow();

            return posInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="loginUser"></param>
        public static void Login(Nt.Data.Session session, Os.Data.LoginUser loginUser)
        {
            bool validWaiter = Nt.Database.DB.Api.Waiter.ValidWaiter(loginUser.Id, loginUser.Password);
            if (!validWaiter)
                throw new Exception(string.Format("user {0} not valid", loginUser.Id));
            Nt.Database.DB.Api.Waiter.Login(session);
            session.WaiterId = loginUser.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public static void Logout(Nt.Data.Session session)
        {
            Nt.Database.DB.Api.Waiter.Logout(session);
        }
    }
}