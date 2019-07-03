using System;
using System.Collections.Generic;
using Os.Server.Client.Model;

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
        List<PubSubTopics> GetTopics ();
        /// <summary>
        /// Publish messages to a topic. The OsServer is providing the following topics + host_staticDataChanged - this topic without any payload (\&quot;\&quot;) will trigger a static data synchronization. 
        /// </summary>
        /// <param name="topic">the topic to publish the message to</param>
        /// <param name="body"></param>
        /// <returns>InlineResponse200</returns>
        InlineResponse200 PubsubTopicsPost (string topic, List<PubSubMessage> body);
        /// <summary>
        /// Subscribe to an existing topic. 
        /// </summary>
        /// <param name="topic">The topic to subscribe to.</param>
        /// <param name="body"></param>
        /// <returns></returns>
        void PubsubTopicsPut (string topic, PubSubSubscription body);
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
        public string BaseUrl {get; set;}
    
        /// <summary>
        /// Provides a list of known topics. 
        /// </summary>
        /// <returns>List&lt;PubSubTopics&gt;</returns>
        public List<PubSubTopics> GetTopics ()
        {
            
            return null;
        }
    
        /// <summary>
        /// Publish messages to a topic. The OsServer is providing the following topics + host_staticDataChanged - this topic without any payload (\&quot;\&quot;) will trigger a static data synchronization. 
        /// </summary>
        /// <param name="topic">the topic to publish the message to</param>
        /// <param name="body"></param>
        /// <returns>InlineResponse200</returns>
        public InlineResponse200 PubsubTopicsPost (string topic, List<PubSubMessage> body)
        {
            return null;
        }
    
        /// <summary>
        /// Subscribe to an existing topic. 
        /// </summary>
        /// <param name="topic">The topic to subscribe to.</param>
        /// <param name="body"></param>
        /// <returns></returns>
        public void PubsubTopicsPut (string topic, PubSubSubscription body)
        {
            return;
        }
    
    }
}
