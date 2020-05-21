using Nt.Booking.Models;
using System;
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
        public NtBooking.BookingSystemType BookingSystem { get => NtBooking.BookingSystemType.ExSI; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediumId"></param>
        /// <param name="metaData"></param>
        /// <returns></returns>
        public async Task<Response> GetMediumInformation(string mediumId, Models.MetaData metaData)
        {
            var client = Controllers.BookingApiController.HttpClientFactory.CreateClient();
            client.BaseAddress = new System.Uri("https://jsonplaceholder.typicode.com");
            var resp = await client.GetStringAsync("/todos/1");

            var information = new InformationResponse();
            return information;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metaData"></param>
        /// <returns></returns>
        public async Task<List<Response>> GetMediumInformation(Models.MetaData metaData)
        {
            throw new NotImplementedException("ExSI GetMediumInformation not implemented");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediumId"></param>
        /// <param name="creditRequest"></param>
        /// <returns></returns>
        public async Task<Response> Credit(string mediumId, Models.CreditRequest creditRequest)
        {
            throw new NotImplementedException("ExSI Credit not implemented");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediumId"></param>
        /// <param name="debitRequest"></param>
        /// <returns></returns>
        public async Task<Response> Debit(string mediumId, Models.DebitRequest debitRequest)
        {
            throw new NotImplementedException("ExSI Debit not implemented");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediumId"></param>
        /// <param name="cancellationRequest"></param>
        /// <returns></returns>
        public async Task<Response> CancelCredit(string mediumId, CancellationRequest cancellationRequest)
        {
            throw new NotImplementedException("ExSI CancelCredit not implemented");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediumId"></param>
        /// <param name="cancellationRequest"></param>
        /// <returns></returns>
        public async Task<Response> CancelDebit(string mediumId, CancellationRequest cancellationRequest)
        {
            throw new NotImplementedException("ExSI CancelDebit not implemented");
        }
    }
}