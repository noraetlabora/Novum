using Nt.Fiscal.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Nt.Fiscal.Models
{
    /// <summary>
    /// This object contains the information of the SendReceipt request
    /// </summary>
    [DataContract]
    public partial class SendReceiptRequest
    {
        /// <summary>
        /// Only check if the receipt data is valid. No fiscalization is performed.
        /// </summary>
        /// <value>true to only perform a validity check. false to send the receipt to the fiscal provider.</value>
        [DataMember(Name = "checkOnly")]
        [Required]
        public bool CheckOnly { get; set; }

        /// <summary>
        /// Information about the client that calls the NT.Fiscal service. Optional provider settings overrides
        /// </summary>
        /// <value>Client details</value>
        [DataMember(Name = "clientInformation")]
        [Required]
        public ClientInformation ClientInformation { get; set; }

        /// <summary>
        /// Optional provider configurationn. If set, these values will override the settings of the service
        /// </summary>
        /// <value>Optional provider condiguration</value>
        [DataMember(Name = "providerConfiguration")]
        public ProviderConfiguration ProviderConfiguration { get; set; }

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
            sb.Append("  CheckOnly: ").Append(CheckOnly.ToString()).Append("\n");
            sb.Append("  ClientInformation: ").Append(ClientInformation.ToString()).Append("\n");
            sb.Append("  ProviderConfiguration: ").Append(ProviderConfiguration.ToString()).Append("\n");
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