using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Nt.Fiscal.Models
{
    /// <summary>
    /// This object contains all information of the SendReceipt request
    /// </summary>
    [DataContract]
    public partial class SendReceiptRequest
    {
        /// <summary>
        /// Information about the client that calls the Nt.Fiscal service. Optional provider settings overrides
        /// </summary>
        /// <value>software version of the requesting system</value>
        [DataMember(Name = "clientInformation")]
        [Required]
        public ClientInformation ClientInformation { get; set; }

        /// <summary>
        /// Information about the receipt
        /// </summary>
        /// <value>receipt information</value>
        [DataMember(Name = "receipt")]
        [Required]
        public Receipt Receipt { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SendReceiptRequest {\n");
            sb.Append("  ClientInformation: ").Append(ClientInformation.ToString()).Append("\n");
            sb.Append("  Receipt: ").Append(Receipt.ToString()).Append("\n");
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