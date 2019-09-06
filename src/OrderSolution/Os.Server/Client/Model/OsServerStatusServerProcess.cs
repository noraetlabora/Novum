using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Os.Server.Client.Model
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class OsServerStatusServerProcess
    {
        /// <summary>
        /// Gets or Sets MemoryUsage
        /// </summary>
        [DataMember(Name = "memoryUsage", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "memoryUsage")]
        public OsServerStatusServerProcessMemoryUsage MemoryUsage { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OsServerStatusServerProcess {\n");
            sb.Append("  MemoryUsage: ").Append(MemoryUsage).Append("\n");
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
