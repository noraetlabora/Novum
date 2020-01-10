
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Os.Server.Client.Model
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class PrintJob
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// The timestamp (Unix Epoch seconds) when this job was created
        /// </summary>
        /// <value>The timestamp (Unix Epoch seconds) when this job was created</value>
        [DataMember(Name = "createdTime", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "createdTime")]
        public int? CreatedTime { get; set; }

        /// <summary>
        /// we support several types of print job status - pending / done / error 
        /// </summary>
        /// <value>we support several types of print job status - pending / done / error </value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// The timestamp (Unix Epoch seconds) when the jobs status was last updated
        /// </summary>
        /// <value>The timestamp (Unix Epoch seconds) when the jobs status was last updated</value>
        [DataMember(Name = "statusTime", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "statusTime")]
        public int? StatusTime { get; set; }

        /// <summary>
        /// Detailed enum about the status; SUCCESS/FAILURE_COVER_OPEN/FAILURE_OUT_OF_PAPER/FAILURE_PRINTER_MALFUNCTION/FAILURE_NO_PRINTER_PAIRED/FAILURE_PRINTER_UNREACHABLE
        /// </summary>
        /// <value>Detailed enum about the status; SUCCESS/FAILURE_COVER_OPEN/FAILURE_OUT_OF_PAPER/FAILURE_PRINTER_MALFUNCTION/FAILURE_NO_PRINTER_PAIRED/FAILURE_PRINTER_UNREACHABLE</value>
        [DataMember(Name = "statusCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "statusCode")]
        public string StatusCode { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PrintJob {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  CreatedTime: ").Append(CreatedTime).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  StatusTime: ").Append(StatusTime).Append("\n");
            sb.Append("  StatusCode: ").Append(StatusCode).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}
