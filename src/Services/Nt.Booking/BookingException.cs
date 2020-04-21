using System;
using System.Text;

namespace Nt.Booking
{
 
    /// <summary>
    /// 
    /// </summary>
    public class BookingException : Exception
    {

        /// <summary> </summary>
        public int HttpStatusCode { get; set; }

        /// <summary> </summary>
        public NtBooking.BookingSystemType BookingSystem { get; set; }

        /// <summary> </summary>
        public string BookingSystemMessage { get; set; }

        /// <summary> </summary>
        public string BookingSystemCode { get; set; }

        /// <summary> </summary>
        public string DisplayMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public BookingException(string message) : base(message)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="httpStatusCode"></param>
        /// <param name="bookingSystem"></param>
        /// <param name="bookingSystemMessage"></param>
        /// <param name="bookingSystemCode"></param>
        /// <param name="displayMessage"></param>
        public BookingException(string message, int httpStatusCode, NtBooking.BookingSystemType bookingSystem, string bookingSystemMessage, string bookingSystemCode, string displayMessage) : base(message)
        {
            this.HttpStatusCode = httpStatusCode;
            this.BookingSystem = bookingSystem;
            this.BookingSystemMessage = bookingSystemMessage;
            this.BookingSystemCode = bookingSystemCode;
            this.DisplayMessage = displayMessage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sB = new StringBuilder();
            sB.Append("BookingException:");
            sB.Append(" HttpStatusCode: ").Append(HttpStatusCode);
            sB.Append(" Message = ").Append(Message);
            sB.Append(" BookingSystem: ").Append(BookingSystem);
            sB.Append(" BookingSystemMessage = ").Append(BookingSystemMessage);
            sB.Append(" BookingSystemCode = ").Append(BookingSystemCode);
            sB.Append(" DisplayMessage = ").Append(DisplayMessage);
            return sB.ToString();
        }
    }
}
