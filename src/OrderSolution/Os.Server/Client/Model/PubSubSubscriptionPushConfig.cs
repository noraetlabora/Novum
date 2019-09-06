using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Os.Server.Client.Model
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class PubSubSubscriptionPushConfig
    {
        /// <summary>
        /// Gets or Sets PushEndpoint
        /// </summary>
        [DataMember(Name = "pushEndpoint", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "pushEndpoint")]
        public string PushEndpoint { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PubSubSubscriptionPushConfig {\n");
            sb.Append("  PushEndpoint: ").Append(PushEndpoint).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Get the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

    }
}
