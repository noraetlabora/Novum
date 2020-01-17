using System;
using System.Text;

namespace Nt.Booking
{
    public class BookingException : Exception
    {

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public string Message { get; set; }
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        public string Code { get; set; }
        public string PartnerMessage { get; set; }
        public string PartnerCode { get; set; }
        public string UserMessage { get; set; }

        public BookingException()
        {

        }

        public BookingException(string message, string code, string partnerMessage, string partnerCode, string userMessage)
        {
            this.Message = message;
            this.Code = code;
            this.PartnerMessage = partnerMessage;
            this.PartnerCode = partnerCode;
            this.UserMessage = userMessage;
        }

        public override string ToString()
        {
            var sB = new StringBuilder();
            sB.Append("BookingException:");
            sB.Append(" Message = ").Append(Message);
            sB.Append(" Code = ").Append(Code);
            sB.Append(" PartnerMessage = ").Append(PartnerMessage);
            sB.Append(" PartnerCode = ").Append(PartnerCode);
            sB.Append(" UserMessage = ").Append(UserMessage);
            return sB.ToString();
        }
    }
}
