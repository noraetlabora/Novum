using System.Runtime.Serialization;
using System.Text;

namespace Nt.Fiscal.Models
{
    /// <summary>
    /// The ProviderConfiguration is optional and can be used to override the service settings.
    /// </summary>
    [DataContract]
    public class ProviderConfiguration
    {
        /// <summary>
        /// Id of the queue. If set, the value will override the settings of the service.
        /// The queue id represents the Cashbox Id in Fiskaltrust, or the combination of the transaction terminal (TT) and transaction location (TL) in EFSTA.
        /// </summary>
        /// <value>Id of the queue</value>
        [DataMember(Name = "queueId")]
        public string QueueId { get; set; }

        /// <summary>
        /// Location of a queue. If set, the value will override the settings of the service.
        /// If set, this URI will be used instead of the URI configured in the service settings.
        /// </summary>
        /// <value>Location/URI of a queue</value>
        [DataMember(Name = "queueLocation")]
        public string QueueLocation { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ProviderConfiguration {\n");
            sb.Append("  QueueId: ").Append(QueueId).Append("\n");
            sb.Append("  QueueLocation: ").Append(QueueLocation).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }

    }
}