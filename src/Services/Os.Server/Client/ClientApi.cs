using Os.Server.Client.Api;
using System.Net.Http;

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
        public static void Initialize(string baseUrl, IHttpClientFactory httpClientFactory)
        {
            Print = new PrintingApi(baseUrl, httpClientFactory);
            Subscribe = new PubsubApi(baseUrl, httpClientFactory);
            Default = new DefaultApi(baseUrl, httpClientFactory);
        }
    }
}
