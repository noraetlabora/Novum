using System;
using System.Runtime.Serialization;
using System.Text;

namespace Nt.Booking.Models
{
    /// <summary>
    /// debit object
    /// </summary>
    [DataContract]
    public partial class Debit
    {
        /// <summary>
        /// debt amount/debt level (*100)
        /// </summary>
        /// <value>debt amount/debt level (*100)</value>
        [DataMember(Name = "amount")]
        public int? Amount { get; set; }

        /// <summary>
        /// debt limit (*100)
        /// </summary>
        /// <value>debt limit (*100)</value>
        [DataMember(Name = "limit")]
        public int? Limit { get; set; }

        /// <summary>
        /// warning of debt limit (*100)
        /// </summary>
        /// <value>warning of debt limit (*100)</value>
        [DataMember(Name = "limitWarning")]
        public int? LimitWarning { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Debit {\n");
            sb.Append("  Amount: ").Append(Amount).Append("\n");
            sb.Append("  Limit: ").Append(Limit).Append("\n");
            sb.Append("  LimitWarning: ").Append(LimitWarning).Append("\n");
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
