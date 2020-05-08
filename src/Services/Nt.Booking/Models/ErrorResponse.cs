using System;
using System.Runtime.Serialization;
using System.Text;

namespace Nt.Booking.Models
{
    /// <summary>
    /// error object
    /// </summary>
    [DataContract]
    public partial class ErrorResponse
    {
        /// <summary>
        /// Gets or Sets Message
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or Sets BookingSystem
        /// </summary>
        [DataMember(Name = "bookingSystem")]
        public string BookingSystem { get; set; }

        /// <summary>
        /// Gets or Sets BookingSystemMessage
        /// </summary>
        [DataMember(Name = "bookingSystemMessage")]
        public string BookingSystemMessage { get; set; }

        /// <summary>
        /// Gets or Sets BookingSystemCode
        /// </summary>
        [DataMember(Name = "bookingSystemCode")]
        public string BookingSystemCode { get; set; }

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
            sb.Append("ErrorResponse {\n");
            sb.Append("  Message: ").Append(Message).Append("\n");
            sb.Append("  BookingSystem: ").Append(BookingSystem).Append("\n");
            sb.Append("  BookingSystemMessage: ").Append(BookingSystemMessage).Append("\n");
            sb.Append("  BookingSystemCode: ").Append(BookingSystemCode).Append("\n");
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
