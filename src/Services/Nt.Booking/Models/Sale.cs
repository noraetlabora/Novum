using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Nt.Booking.Models
{
    /// <summary>
    /// This object contains all information of a sale
    /// </summary>
    [DataContract]
    public partial class Sale
    {
        /// <summary>
        /// ID of the bill
        /// </summary>
        /// <value>ID of the bill</value>
        [DataMember(Name = "billId")]
        public string BillId { get; set; }

        /// <summary>
        /// Creation date and time of the bill
        /// </summary>
        /// <value>Creation date and time of the bill</value>
        [DataMember(Name = "timestamp")]
        public DateTime? Timestamp { get; set; }

        /// <summary>
        /// currency in ISO 4217 (EUR/CHF/GBP/USD/JPY/CNY/...)
        /// </summary>
        /// <value>currency in ISO 4217 (EUR/CHF/GBP/USD/JPY/CNY/...)</value>
        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Gets or Sets Payments
        /// </summary>
        //[DataMember(Name = "payments")]
        //public List<Payment> Payments { get; set; }

        /// <summary>
        /// Gets or Sets Articles
        /// </summary>
        [DataMember(Name = "articles")]
        public List<Article> Articles { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Sale {\n");
            sb.Append("  BillId: ").Append(BillId).Append("\n");
            sb.Append("  Timestamp: ").Append(Timestamp).Append("\n");
            sb.Append("  Currency: ").Append(Currency).Append("\n");
            //sb.Append("  Payments: ").Append(Payments).Append("\n");
            sb.Append("  Articles: ").Append(Articles).Append("\n");
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
