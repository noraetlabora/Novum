using System.Runtime.Serialization;

namespace Nt.Booking.Models
{
    /// <summary>
    /// This object contains all Information of the medium
    /// </summary>
    [DataContract]
    public partial class InformationResponse : Response
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
        public DebitResponse Debit { get; set; }

        /// <summary>
        /// Gets or Sets Credit
        /// </summary>
        [DataMember(Name = "credit")]
        public CreditResponse Credit { get; set; }

        /// <summary>
        /// currency in ISO 4217 (EUR/CHF/GBP/USD/JPY/CNY/...)
        /// </summary>
        /// <value>currency in ISO 4217 (EUR/CHF/GBP/USD/JPY/CNY/...)</value>
        [DataMember(Name = "currency")]
        public string Currency { get; set; }


        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public override string ToJson()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
