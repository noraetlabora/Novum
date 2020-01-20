using Nt.Booking.Models;
using System.Collections.Generic;

namespace Nt.Booking.Systems.Access.ExSI
{
    public class ExSI : BookingSystemBase
    {

        public ExSI()
        {
            BookingSystem = NtBooking.BookingSystemType.ExSI;
        }

        public override Models.Booking Cancel()
        {
            var ex = new BookingException("ExSI method 'cancel' is not yet implemented");
            ex.HttpStatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status501NotImplemented;
            throw ex;
        }

        public override Information GetMediumInformation(string mediumId)
        {
            var information = new Information();
            var owner = new Owner();
            owner.Name = "Norbert Rastl";
            var credit = new Credit();
            credit.Amount = 1499;
            credit.OpeningAmount = 2000;
            information.Owner = owner;
            information.Credit = credit;

            return information;
        }

        public override List<Information> GetMediumInformation()
        {
            var ex = new BookingException("ExSI can't return a list of medium information");
            ex.HttpStatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status405MethodNotAllowed;
            throw ex;
        }

        public override Models.Booking Pay()
        {
            var ex = new BookingException("ExSI method 'pay' is not yet implemented");
            ex.HttpStatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status501NotImplemented;
            throw ex;
        }
    }
}
