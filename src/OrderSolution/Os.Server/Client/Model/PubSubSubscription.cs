using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Os.Server.Client.Model
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class PubSubSubscription
    {
        /// <summary>
        /// Gets or Sets PushConfig
        /// </summary>
        [DataMember(Name = "pushConfig", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "pushConfig")]
        public PubSubSubscriptionPushConfig PushConfig { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PubSubSubscription {\n");
            sb.Append("  PushConfig: ").Append(PushConfig).Append("\n");
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
