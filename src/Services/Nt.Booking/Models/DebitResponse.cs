using System.Runtime.Serialization;

namespace Nt.Booking.Models
{
    /// <summary>
    /// debit object
    /// </summary>
    [DataContract]
    public partial class DebitResponse : Response
    {
        /// <summary>
        /// debt amount/debt level
        /// </summary>
        /// <value>debt amount/debt level</value>
        [DataMember(Name = "amount")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// debt limit
        /// </summary>
        /// <value>debt limit</value>
        [DataMember(Name = "limit")]
        public decimal? Limit { get; set; }

        /// <summary>
        /// warning of debt limit
        /// </summary>
        /// <value>warning of debt limit</value>
        [DataMember(Name = "limitWarning")]
        public decimal? LimitWarning { get; set; }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public override string ToJson()
        {
            //return JsonConvert.SerializeObject(this, Formatting.Indented);
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
