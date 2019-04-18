using System;
using System.Collections.Generic;
using Novum.Data;
using Novum.Data.Os;
using Novum.Database;


namespace Novum.Logic.Os
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
        public static POSInfo RegisterClient(Session session, ClientInfo clientData)
        {
            var posId = DB.Api.Pos.GetPosId(clientData.Id);
            if (string.IsNullOrEmpty(posId))
                throw new Exception(string.Format("client {0} not valid", clientData.Id));
            var serviceAreaId = DB.Api.Pos.GetServiceAreaId(posId);

            var posInfo = new POSInfo();
            posInfo.RestaurantName = DB.Api.Pos.GetServiceAreaName(serviceAreaId);
            posInfo.ClientName = "Orderman";
            posInfo.UtcTime = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            return posInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="loginUser"></param>
        public static void Login(Session session, LoginUser loginUser)
        {

            bool validWaiter = DB.Api.Waiter.ValidWaiter(loginUser.Id, loginUser.Password);
            if (!validWaiter)
                throw new Exception(string.Format("user {0} not valid", loginUser.Id));
            DB.Api.Waiter.Login(session);
            session.WaiterId = loginUser.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public static void Logout(Session session)
        {
            DB.Api.Waiter.Logout(session);
        }
    }
}
