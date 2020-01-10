
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Os.Server.Client.Model
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class OsServerStatus
    {
        /// <summary>
        /// Gets or Sets Server
        /// </summary>
        [DataMember(Name = "server", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "server")]
        public OsServerStatusServer Server { get; set; }

        /// <summary>
        /// Gets or Sets Clients
        /// </summary>
        [DataMember(Name = "clients", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "clients")]
        public List<ClientStatus> Clients { get; set; }

        /// <summary>
        /// Gets or Sets Hosts
        /// </summary>
        [DataMember(Name = "hosts", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "hosts")]
        public List<HostStatus> Hosts { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OsServerStatus {\n");
            sb.Append("  Server: ").Append(Server).Append("\n");
            sb.Append("  Clients: ").Append(Clients).Append("\n");
            sb.Append("  Hosts: ").Append(Hosts).Append("\n");
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
