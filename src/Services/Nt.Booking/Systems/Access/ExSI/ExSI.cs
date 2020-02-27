using Nt.Booking.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nt.Booking.Systems.Access
{
    public class ExSI : IBookingSystem
    {
        public ExSI()
        {
        }

        public NtBooking.BookingSystemType Type { get => NtBooking.BookingSystemType.ExSI; }

        Task<BookingResponse> IBookingSystem.Cancel(CancellationRequest cancellationRequest)
        {
            var ex = new BookingException("ExSI method 'cancel' is not yet implemented");
            ex.HttpStatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status501NotImplemented;
            throw ex;
        }

        async Task<InformationResponse> IBookingSystem.GetMediumInformation(string mediumId)
        {
            var client = Controllers.BookingApiController.HttpClientFactory.CreateClient();
            client.BaseAddress = new System.Uri("https://jsonplaceholder.typicode.com");
            var resp = await client.GetStringAsync("/todos/1");

            var information = new InformationResponse();
            var owner = new Owner();
            owner.Name = "Norbert Rastl"; // resp;
            var credit = new Credit();
            credit.Amount = 1499;
            credit.OpeningAmount = 2000;
            information.Owner = owner;
            information.Credit = credit;
            information.Currency = "EUR";
            return information;
        }

        Task<List<InformationResponse>> IBookingSystem.GetMediumInformation()
        {
            var ex = new BookingException("ExSI can't return a list of medium information");
            ex.HttpStatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status405MethodNotAllowed;
            throw ex;
        }

        Task<BookingResponse> IBookingSystem.Pay(PaymentRequest paymentRequest)
        {
            var ex = new BookingException("ExSI method 'pay' is not yet implemented");
            ex.HttpStatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status501NotImplemented;
            throw ex;
        }
    }
}