using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Nt.Fiscal.Models
{
    /// <summary>
    /// This object contains all information of the Client
    /// </summary>
    [DataContract]
    public class ClientInformation
    {
        /// <summary>
        /// software version of the requesting system
        /// </summary>
        /// <value>software version of the requesting system</value>
        /// <remarks>Fuck you</remarks>
        [DataMember(Name = "applicationVersion")]
        [Required]
        public string ApplicationVersion { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ClientInformation {\n");
            sb.Append("  ApplicationVersion: ").Append(ApplicationVersion).Append("\n");
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