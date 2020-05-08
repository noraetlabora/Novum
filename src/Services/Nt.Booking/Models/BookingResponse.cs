using System;
using System.Runtime.Serialization;
using System.Text;
namespace Nt.Booking.Models
{
    /// <summary>
    /// This object contains all information of a booking (payment/cancellation)
    /// </summary>
    [DataContract]
    public partial class BookingResponse
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// message wich should be displayed (eg. information of the medium)
        /// </summary>
        /// <value>message wich should be displayed (eg. information of the medium)</value>
        [DataMember(Name = "displayMessage")]
        public string DisplayMessage { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("BookingResponse {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  DisplayMessage: ").Append(DisplayMessage).Append("\n");
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
