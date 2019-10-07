
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Os.Server.Client.Model
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Printer
    {
        /// <summary>
        /// The printers id we can identify it with; this must only be unique on this server and must not be interpreted
        /// </summary>
        /// <value>The printers id we can identify it with; this must only be unique on this server and must not be interpreted</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// The printer type is providing information which protocol this printer supports. ATTENTION: use contains for the string and not 1to1 string comparison as the type string can have additional characters
        /// </summary>
        /// <value>The printer type is providing information which protocol this printer supports. ATTENTION: use contains for the string and not 1to1 string comparison as the type string can have additional characters</value>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Printer {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
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
