using Nt.Booking.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nt.Booking.Systems.Access.ExSI
{
    public class ExSI : BookingSystemBase
    {

        public ExSI()
        {
            Type = NtBooking.BookingSystemType.ExSI;
        }

        public override BookingResponse Cancel(CancellationRequest cancellationRequest)
        {
            var ex = new BookingException("ExSI method 'cancel' is not yet implemented");
            ex.HttpStatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status501NotImplemented;
            throw ex;
        }

        public override async Task<InformationResponse> GetMediumInformation(string mediumId)
        {
            var client = Controllers.BookingApiController.HttpClientFactory.CreateClient();
            client.BaseAddress = new System.Uri("https://rest.ensemble.org/info/ping");
            //var response = Task.Run(() => client.GetAsync("/"));
            var response = client.GetAsync("/");

            var information = new InformationResponse();
            var owner = new Owner();
            owner.Name = response.Result.Content.ToString(); // "XYZ";
            var credit = new Credit();
            credit.Amount = 1499;
            credit.OpeningAmount = 2000;
            information.Owner = owner;
            information.Credit = credit;
            information.Currency = "EUR";
            return information;
        }

        public override List<InformationResponse> GetMediumInformation()
        {
            var ex = new BookingException("ExSI can't return a list of medium information");
            ex.HttpStatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status405MethodNotAllowed;
            throw ex;
        }

        public override BookingResponse Pay(PaymentRequest paymentRequest)
        {
            var ex = new BookingException("ExSI method 'pay' is not yet implemented");
            ex.HttpStatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status501NotImplemented;
            throw ex;
        }
    }
}
