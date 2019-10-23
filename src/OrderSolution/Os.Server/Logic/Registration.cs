using System;

namespace Os.Server.Logic
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
        public static Models.RegisterClientResponse RegisterClient(Nt.Data.Session session, Models.ClientInfo clientData)
        {
            CheckDevice(clientData.Id);

            var registerClientResponse = new Models.RegisterClientResponse();
            registerClientResponse.ClientName = "Orderman";
            registerClientResponse.UtcTime = Nt.Data.Utils.Unix.TimestampNow();

            return registerClientResponse;
        }

        public static void CheckDevice(string serialNumber)
        {
            var posId = Nt.Database.DB.Api.Pos.GetPosId(serialNumber);
            if (string.IsNullOrEmpty(posId))
                throw new Exception(string.Format(Resources.Dictionary.GetString("Device_NotValid"), serialNumber));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="loginUser"></param>
        public static void Login(Nt.Data.Session session, Models.LoginUser loginUser)
        {
            bool validWaiter = Nt.Database.DB.Api.Waiter.ValidWaiter(loginUser.Id, loginUser.Password);
            if (!validWaiter)
                throw new Exception(string.Format(Resources.Dictionary.GetString("Waiter_NotValid"), loginUser.Id));

            Nt.Database.DB.Api.Waiter.Login(session);
            session.WaiterId = loginUser.Id;
            var permissions = Nt.Database.DB.Api.Waiter.GetPermissions(loginUser.Id);
            session.SetPermissions(permissions);
            Image.RemoveImages(session);
            Fiscal.CheckSystem(session);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public static void Logout(Nt.Data.Session session)
        {
            Nt.Database.DB.Api.Waiter.Logout(session);
            Image.RemoveImages(session);
        }
    }
}