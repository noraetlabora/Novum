using System;
using System.Text;

namespace Nt.Booking
{
    public class BookingException : Exception
    {

        public int HttpStatusCode { get; set; }
        public NtBooking.BookingSystemType BookingSystem { get; set; }
        public string BookingSystemMessage { get; set; }
        public string BookingSystemCode { get; set; }
        public string DisplayMessage { get; set; }

        public BookingException(string message) : base(message)
        {

        }

        public BookingException(string message, int httpStatusCode, NtBooking.BookingSystemType bookingSystem, string bookingSystemMessage, string bookingSystemCode, string displayMessage) : base(message)
        {
            this.HttpStatusCode = httpStatusCode;
            this.BookingSystem = bookingSystem;
            this.BookingSystemMessage = bookingSystemMessage;
            this.BookingSystemCode = bookingSystemCode;
            this.DisplayMessage = displayMessage;
        }

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
