using System;
using System.Runtime.Serialization;
using System.Text;

namespace Nt.Booking.Models
{
    /// <summary>
    /// This object contains all Information of the medium
    /// </summary>
    [DataContract]
    public partial class InformationResponse
    {
        /// <summary>
        /// Gets or Sets Owner
        /// </summary>
        /// <value>xxxx</value>
        [DataMember(Name = "owner")]
        public Owner Owner { get; set; }

        /// <summary>
        /// Gets or Sets Debit
        /// </summary>
        /// <value>yyyy</value>
        [DataMember(Name = "debit")]
        public Debit Debit { get; set; }

        /// <summary>
        /// Gets or Sets Credit
        /// </summary>
        [DataMember(Name = "credit")]
        public Credit Credit { get; set; }

        /// <summary>
        /// currency in ISO 4217 (EUR/CHF/GBP/USD/JPY/CNY/...)
        /// </summary>
        /// <value>currency in ISO 4217 (EUR/CHF/GBP/USD/JPY/CNY/...)</value>
        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        /// <summary>
        /// type of discount
        /// </summary>
        /// <value>type of discount</value>
        [DataMember(Name = "discountType")]
        public string DiscountType { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class InformationResponse {\n");
            sb.Append("  Owner: ").Append(Owner).Append("\n");
            sb.Append("  Debit: ").Append(Debit).Append("\n");
            sb.Append("  Credit: ").Append(Credit).Append("\n");
            sb.Append("  Currency: ").Append(Currency).Append("\n");
            sb.Append("  DiscountType: ").Append(DiscountType).Append("\n");
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
