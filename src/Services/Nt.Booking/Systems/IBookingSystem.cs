using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nt.Booking.Systems
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBookingSystem
    {
        /// <summary>
        /// 
        /// </summary>
        public NtBooking.BookingSystemType Type { get; }

        /// <summary>
        /// Get the information to one medium (e.g. chip/room/voucher/...)
        /// </summary>
        /// <param name="mediumId"></param>
        /// <returns></returns>
        public Task<Models.InformationResponse> GetMediumInformation(string mediumId);

        /// <summary>
        /// Get a list of information to all medium (e.g. list of rooms)
        /// </summary>
        /// <returns></returns>
        public Task<List<Models.InformationResponse>> GetMediumInformation();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="creditRequest"></param>
        /// <returns></returns>
        public Task<Models.BookingResponse> Credit(Models.CreditRequest creditRequest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="debitRequest"></param>
        /// <returns></returns>
        public Task<Models.BookingResponse> Debit(Models.DebitRequest debitRequest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationRequest"></param>
        /// <returns></returns>
        public Task<Models.BookingResponse> CancelCredit(Models.CancellationRequest cancellationRequest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationRequest"></param>
        /// <returns></returns>
        public Task<Models.BookingResponse> CancelDebit(Models.CancellationRequest cancellationRequest);
    }
}