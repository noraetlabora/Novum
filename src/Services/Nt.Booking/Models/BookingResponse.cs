using System.Runtime.Serialization;
namespace Nt.Booking.Models
{
    /// <summary>
    /// This object contains all information of a booking (payment/cancellation)
    /// </summary>
    [DataContract]
    public partial class BookingResponse : Response
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [DataMember(Name = "approvedAmount")]
        public decimal? ApprovedAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [DataMember(Name = "balanceAmount")]
        public decimal? BalanceAmount { get; set; }

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
