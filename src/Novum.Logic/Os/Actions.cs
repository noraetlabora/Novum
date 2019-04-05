using System;
using System.Collections.Generic;
using Novum.Data.Os;
using Novum.Database;


namespace Novum.Logic.Os
{
    /// <summary>
    /// 
    /// </summary>
    public class Actions
    {

        #region Initialization

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientData"></param>
        /// <returns></returns>
        public static POSInfo RegisterClient(ClientInfo clientData)
        {
            var posId = DB.Api.Pos.GetPosId(clientData.Id);
            if (string.IsNullOrEmpty(posId))
                throw new Exception(string.Format("client {0} not valid", clientData.Id));
            var serviceAreaId = DB.Api.Pos.GetServiceAreaId(posId);

            var posInfo = new Novum.Data.Os.POSInfo();
            posInfo.RestaurantName = DB.Api.Pos.GetServiceAreaName(serviceAreaId);
            posInfo.ClientName = "Orderman";
            posInfo.UtcTime = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            return posInfo;
        }

        #endregion

        #region Authentication

        public static void Login(LoginUser loginUser)
        {
            bool validWaiter = DB.Api.Waiter.ValidWaiter(loginUser.Id, loginUser.Password);
            if (!validWaiter)
                throw new Exception(string.Format("user {0} not valid", loginUser.Id));
            DB.Api.Waiter.Login("125-49787459", loginUser.Id);
        }

        public static void Logout(string waiterId)
        {
        }

        #endregion




    }
}