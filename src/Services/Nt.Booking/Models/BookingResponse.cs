using System;
using System.Runtime.Serialization;
using System.Text;
namespace Nt.Booking.Models
{
    /// <summary>
    /// This object contains all information of a booking (payment/cancellation)
    /// </summary>
    [DataContract]
    public partial class BookingResponse : Response
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }
     

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
