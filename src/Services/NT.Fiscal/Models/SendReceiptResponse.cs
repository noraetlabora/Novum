using System.Runtime.Serialization;
using System.Text;

namespace NT.Fiscal.Models
{
    /// <summary>
    /// This object contains response information of the SendReceipt
    /// </summary>
    [DataContract]
    public partial class SendReceiptResponse
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SendReceiptResponse {\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
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