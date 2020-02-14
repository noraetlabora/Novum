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
        public static async Task<object> GetProvider(Nt.Data.Session session)
        {
            var fiscalProvider = await Nt.Database.DB.Api.Fiscal.GetProvider(session);
            if (fiscalProvider == null)
                Nt.Logging.Log.Server.Error(string.Format("no fiscal provider found for {0}, {1}, {2}", session.ClientId, session.PosId, session.SerialNumber));
            else
                Nt.Logging.Log.Server.Info(string.Format("fiscal provider found for {0}, {1}, {2}", session.ClientId, session.PosId, session.SerialNumber));

            return fiscalProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public static async Task CheckSystem(Nt.Data.Session session)
        {
            var statusString = await Nt.Database.DB.Api.Fiscal.CheckSystem(session);

            if (!string.IsNullOrEmpty(statusString))
                Nt.Logging.Log.Server.Info(statusString);
        }
    }
}
