using System;
using System.Runtime.Serialization;
using System.Text;

namespace Nt.Booking.Models
{
    /// <summary>
    /// credit object
    /// </summary>
    [DataContract]
    public partial class Credit
    {
        /// <summary>
        /// credit balance (*100)
        /// </summary>
        /// <value>credit balance (*100)</value>
        [DataMember(Name = "amount")]
        public int? Amount { get; set; }

        /// <summary>
        /// credit balance at opening (*100)
        /// </summary>
        /// <value>credit balance at opening (*100)</value>
        [DataMember(Name = "openingAmount")]
        public int? OpeningAmount { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Credit {\n");
            sb.Append("  Amount: ").Append(Amount).Append("\n");
            sb.Append("  OpeningAmount: ").Append(OpeningAmount).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            //return JsonConvert.SerializeObject(this, Formatting.Indented);
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
