using System;
using System.Threading.Tasks;

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
            _ = CheckDevice(clientData.Id);

            var registerClientResponse = new Models.RegisterClientResponse();
            registerClientResponse.ClientName = "Orderman";
            registerClientResponse.UtcTime = Nt.Data.Utils.Unix.TimestampNow();

            return registerClientResponse;
        }

        public static async Task CheckDevice(string serialNumber)
        {
            var posId = await Nt.Database.DB.Api.Pos.GetPosId(serialNumber).ConfigureAwait(false);
            if (string.IsNullOrEmpty(posId))
                throw new Exception(string.Format(Resources.Dictionary.GetString("DeviceNotValid"), serialNumber));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="loginUser"></param>
        public static async Task Login(Nt.Data.Session session, Models.LoginUser loginUser)
        {
            // login over pin
            if (string.IsNullOrEmpty(loginUser.Password))
            {
                var waiterId = await Nt.Database.DB.Api.Waiter.GetWaiterId(loginUser.Id).ConfigureAwait(false);
                if (string.IsNullOrEmpty(waiterId))
                    throw new Exception(Resources.Dictionary.GetString("WaiterPinNotValid"));

                session.WaiterId = waiterId;
            }
            // login over waiter selection
            else
            {
                var validWaiter = await Nt.Database.DB.Api.Waiter.ValidWaiter(loginUser.Id, loginUser.Password).ConfigureAwait(false);
                if (!validWaiter)
                    throw new Exception(Resources.Dictionary.GetString("WaiterIdPasswordNotValid"));

                session.WaiterId = loginUser.Id;
            }

            await Nt.Database.DB.Api.Waiter.Login(session).ConfigureAwait(false);
            var permissions = await Nt.Database.DB.Api.Waiter.GetPermissions(loginUser.Id).ConfigureAwait(false);
            session.SetPermissions(permissions);
            Image.RemoveImages(session);
            await Fiscal.CheckSystem(session).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public static async Task Logout(Nt.Data.Session session)
        {
            await Nt.Database.DB.Api.Waiter.Logout(session).ConfigureAwait(false);
            Image.RemoveImages(session);
        }
    }
}