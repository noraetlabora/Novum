using System;
using System.Text;

namespace Nt.Booking
{
    public class BookingException : Exception
    {

        public int HttpStatusCode { get; set; }
        public string Code { get; set; }
        public string PartnerMessage { get; set; }
        public string PartnerCode { get; set; }
        public string UserMessage { get; set; }

        public BookingException(string message) : base(message)
        {
            //test
        }

        public BookingException(string message, int httpStatusCode, string code, string partnerMessage, string partnerCode, string userMessage) : base(message)
        {
            this.HttpStatusCode = httpStatusCode;
            this.Code = code;
            this.PartnerMessage = partnerMessage;
            this.PartnerCode = partnerCode;
            this.UserMessage = userMessage;
        }

        public override string ToString()
        {
            var sB = new StringBuilder();
            sB.Append("BookingException:");
            sB.Append(" HttpStatusCode: ").Append(HttpStatusCode);
            sB.Append(" Message = ").Append(Message);
            sB.Append(" Code = ").Append(Code);
            sB.Append(" PartnerMessage = ").Append(PartnerMessage);
            sB.Append(" PartnerCode = ").Append(PartnerCode);
            sB.Append(" UserMessage = ").Append(UserMessage);
            return sB.ToString();
        }
    }
}
