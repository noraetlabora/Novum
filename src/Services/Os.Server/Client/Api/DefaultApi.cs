using Os.Server.Client.Model;
using System;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultApi"/> class.
        /// </summary>
        /// <returns></returns>
        public DefaultApi(String baseUrl)
        {
            BaseUrl = baseUrl;
        }

        /// <summary>
        /// Gets or sets the API client.
        /// </summary>
        /// <value>An instance of the ApiClient</value>
        public string BaseUrl { get; private set; }

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
