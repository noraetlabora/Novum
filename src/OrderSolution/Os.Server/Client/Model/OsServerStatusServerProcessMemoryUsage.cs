using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Os.Server.Client.Model
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class OsServerStatusServerProcessMemoryUsage
    {
        /// <summary>
        /// Resident Set Size in bytes
        /// </summary>
        /// <value>Resident Set Size in bytes</value>
        [DataMember(Name = "rss", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "rss")]
        public int? Rss { get; set; }

        /// <summary>
        /// engine memory total in bytes
        /// </summary>
        /// <value>engine memory total in bytes</value>
        [DataMember(Name = "heapTotal", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "heapTotal")]
        public int? HeapTotal { get; set; }

        /// <summary>
        /// engine memory used in bytes
        /// </summary>
        /// <value>engine memory used in bytes</value>
        [DataMember(Name = "heapUsed", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "heapUsed")]
        public int? HeapUsed { get; set; }

        /// <summary>
        /// native memory used for objects bound to the engine in bytes
        /// </summary>
        /// <value>native memory used for objects bound to the engine in bytes</value>
        [DataMember(Name = "external", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "external")]
        public int? External { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OsServerStatusServerProcessMemoryUsage {\n");
            sb.Append("  Rss: ").Append(Rss).Append("\n");
            sb.Append("  HeapTotal: ").Append(HeapTotal).Append("\n");
            sb.Append("  HeapUsed: ").Append(HeapUsed).Append("\n");
            sb.Append("  External: ").Append(External).Append("\n");
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
