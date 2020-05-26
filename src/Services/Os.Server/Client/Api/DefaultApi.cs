using Os.Server.Client.Model;
using System;
using System.Net.Http;

namespace Os.Server.Client.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IDefaultApi
    {
        /// <summary>
        /// Provides status information about the OsServer and conencted systems Used to get information about the current state of the system. 
        /// </summary>
        /// <returns>OsServerStatus</returns>
        OsServerStatus GetStatus();
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class DefaultApi : IDefaultApi
    {
        private readonly string _baseUrl;
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultApi"/> class.
        /// </summary>
        /// <returns></returns>
        public DefaultApi(String baseUrl, IHttpClientFactory httpClientFactory)
        {
            _baseUrl = baseUrl;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Provides status information about the OsServer and conencted systems Used to get information about the current state of the system. 
        /// </summary>
        /// <returns>OsServerStatus</returns>
        public OsServerStatus GetStatus()
        {
            return null;
        }

    }
}
