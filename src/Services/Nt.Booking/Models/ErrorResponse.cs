using System.Runtime.Serialization;
using System.Text;

namespace Nt.Booking.Models
{
    /// <summary>
    /// error object
    /// </summary>
    [DataContract]
    public partial class ErrorResponse : Response
    {
     
        /// <summary>
        /// 
        /// </summary>
        public ErrorResponse()
        {
            this.Error = new ErrorResponseError();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookingSystem"></param>
        public ErrorResponse(string bookingSystem)
        {
            this.Error = new ErrorResponseError();
            this.Error.BookingSystem = bookingSystem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookingSystem"></param>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <param name="partnerCode"></param>
        /// <param name="partnerMessage"></param>
        public ErrorResponse(string bookingSystem, Enums.ErrorCode statusCode, string message, string partnerCode, string partnerMessage)
        {
            this.Error = new ErrorResponseError();
            this.Error.BookingSystem = bookingSystem;
            this.Error.Code = statusCode;
            this.Error.Message = message;
            this.Error.PartnerCode = partnerCode;
            this.Error.PartnerMessage = partnerMessage;
        }
        /// <summary>
        /// message wich should be displayed (eg. information of the medium)
        /// </summary>
        /// <value>message wich should be displayed (eg. information of the medium)</value>
        [DataMember(Name = "error")]
        public ErrorResponseError Error { get; set; }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public override string ToJson()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class ErrorResponseError
    {
        /// <summary>
        /// Gets or Sets BookingSystem
        /// </summary>
        [DataMember(Name = "bookingSystem")]
        public string BookingSystem { get; set; }

        /// <summary>
        /// Gets or Sets Error Code
        /// </summary>
        [DataMember(Name = "code")]
        public Enums.ErrorCode? Code { get; set; }

        /// <summary>
        /// Gets or Sets Error Message
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or Sets BookingSystemMessage
        /// </summary>
        [DataMember(Name = "partnerMessage")]
        public string PartnerMessage { get; set; }

        /// <summary>
        /// Gets or Sets BookingSystemCode
        /// </summary>
        [DataMember(Name = "partnerCode")]
        public string PartnerCode { get; set; }
    }
}
