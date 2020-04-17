using Nt.Booking.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nt.Booking.Systems.Access
{
    /// <summary>
    /// 
    /// </summary>
    public class ExSI : IBookingSystem
    {
        /// <summary>
        /// 
        /// </summary>
        public ExSI()
        {
        }

        /// <summary> </summary>
        public NtBooking.BookingSystemType Type { get => NtBooking.BookingSystemType.ExSI; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediumId"></param>
        /// <returns></returns>
        public async Task<InformationResponse> GetMediumInformation(string mediumId)
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<InformationResponse>> GetMediumInformation()
        {
            var ex = new BookingException("ExSI can't return a list of medium information");
            ex.HttpStatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status405MethodNotAllowed;
            throw ex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="creditRequest"></param>
        /// <returns></returns>
        public async Task<BookingResponse> Credit(Models.CreditRequest creditRequest)
        {
            var ex = new BookingException("ExSI method 'credit' is not yet implemented");
            ex.HttpStatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status501NotImplemented;
            throw ex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="debitRequest"></param>
        /// <returns></returns>
        public async Task<BookingResponse> Debit(Models.DebitRequest  debitRequest)
        {
            var ex = new BookingException("ExSI method 'debit' is not yet implemented");
            ex.HttpStatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status501NotImplemented;
            throw ex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationRequest"></param>
        /// <returns></returns>
        public async Task<BookingResponse> CancelCredit(CancellationRequest cancellationRequest)
        {
            var ex = new BookingException("ExSI method 'cancel' is not yet implemented");
            ex.HttpStatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status501NotImplemented;
            throw ex;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationRequest"></param>
        /// <returns></returns>
        public async Task<BookingResponse> CancelDebit(CancellationRequest cancellationRequest)
        {
            var ex = new BookingException("ExSI method 'cancel' is not yet implemented");
            ex.HttpStatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status501NotImplemented;
            throw ex;
        }
    }
}