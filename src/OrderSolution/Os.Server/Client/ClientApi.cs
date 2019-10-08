using Os.Server.Client.Api;

namespace Os.Server.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientApi
    {

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public static IPrintingApi Print { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public static Os.Server.Client.Api.IPubsubApi Subscribe { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public static Os.Server.Client.Api.IDefaultApi Default { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUrl"></param>
        public ClientApi(string baseUrl)
        {
            Print = new PrintingApi(baseUrl);
            Subscribe = new PubsubApi(baseUrl);
            Default = new DefaultApi(baseUrl);
        }
    }
}
