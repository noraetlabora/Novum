using Newtonsoft.Json;
using Os.Server.Client.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Os.Server.Client.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IPubsubApi
    {
        /// <summary>
        /// Provides a list of known topics. 
        /// </summary>
        /// <returns>List&lt;PubSubTopics&gt;</returns>
        List<PubSubTopics> GetTopics();
        /// <summary>
        /// Publish messages to a topic. The OsServer is providing the following topics + host_staticDataChanged - this topic without any payload (\&quot;\&quot;) will trigger a static data synchronization. 
        /// </summary>
        /// <param name="topic">the topic to publish the message to</param>
        /// <param name="body"></param>
        /// <returns>InlineResponse200</returns>
        void PubsubTopicsPost(string topic, List<PubSubMessage> body);
        /// <summary>
        /// Subscribe to an existing topic. 
        /// </summary>
        /// <param name="topic">The topic to subscribe to.</param>
        /// <param name="body"></param>
        /// <returns></returns>
        void PubsubTopicsPut(string topic, PubSubSubscription body);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class PubsubApi : IPubsubApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PubsubApi"/> class.
        /// </summary>
        /// <returns></returns>
        public PubsubApi(String baseUrl)
        {
            BaseUrl = baseUrl;
        }

        /// <summary>
        /// Gets or sets the API client.
        /// </summary>
        /// <value>An instance of the ApiClient</value>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Provides a list of known topics. 
        /// </summary>
        /// <returns>List&lt;PubSubTopics&gt;</returns>
        public List<PubSubTopics> GetTopics()
        {

            return null;
        }

        /// <summary>
        /// Publish messages to a topic. The OsServer is providing the following topics + host_staticDataChanged - this topic without any payload (\&quot;\&quot;) will trigger a static data synchronization. 
        /// </summary>
        /// <param name="topic">the topic to publish the message to</param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async void PubsubTopicsPost(string topic, List<PubSubMessage> body)
        {
            var requestPath = "/api/v1/pubsub/topics/" + topic;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var jsonString = JsonConvert.SerializeObject(body);
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    var requestIdentifier = new Microsoft.AspNetCore.Http.Features.HttpRequestIdentifierFeature();

                    var sb = new StringBuilder();
                    sb.Append("Server Request ").Append("|");
                    sb.Append("125-00000000").Append("|");
                    sb.Append(requestIdentifier.TraceIdentifier).Append("|");
                    sb.Append("POS").Append("|");
                    sb.Append(requestPath).Append("|");
                    sb.Append(jsonString);
                    Nt.Logging.Log.Communication.Info(sb.ToString());

                    var requestUri = BaseUrl + requestPath;
                    var response = await httpClient.PostAsync(requestUri, content);

                    sb = new StringBuilder();
                    sb.Append("Client Response").Append("|");
                    sb.Append("125-00000000").Append("|");
                    sb.Append(requestIdentifier.TraceIdentifier).Append("|");
                    sb.Append((int)response.StatusCode).Append("|");
                    Nt.Logging.Log.Communication.Info(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                Nt.Logging.Log.Server.Error(ex, requestPath);
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return;
        }

        /// <summary>
        /// Subscribe to an existing topic. 
        /// </summary>
        /// <param name="topic">The topic to subscribe to.</param>
        /// <param name="body"></param>
        /// <returns></returns>
        public void PubsubTopicsPut(string topic, PubSubSubscription body)
        {
            return;
        }

    }
}
