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
        /// <param name="metaData"></param>
        /// <param name="mediumId"></param>
        /// <returns></returns>
        public Task<Models.InformationResponse> GetMediumInformation(string mediumId, Models.MetaData metaData);

        /// <summary>
        /// Get a list of information to all medium (e.g. list of rooms)
        /// </summary>
        /// <param name="metaData"></param>
        /// <returns></returns>
        public Task<List<Models.InformationResponse>> GetMediumInformation(Models.MetaData metaData);

        /// <summary>
        /// AUFBUCHEN / GUTSCHREIBEN / 
        /// </summary>
        /// <param name="mediumId"></param>
        /// <param name="creditRequest"></param>
        /// <returns></returns>
        public Task<Models.BookingResponse> Credit(string mediumId, Models.CreditRequest creditRequest);

        /// <summary>
        /// ABBUCHEN / BELASTEN / 
        /// </summary>
        /// <param name="mediumId"></param>
        /// <param name="debitRequest"></param>
        /// <returns></returns>
        public Task<Models.BookingResponse> Debit(string mediumId, Models.DebitRequest debitRequest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediumId"></param>
        /// <param name="cancellationRequest"></param>
        /// <returns></returns>
        public Task<Models.BookingResponse> CancelCredit(string mediumId, Models.CancellationRequest cancellationRequest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediumId"></param>
        /// <param name="cancellationRequest"></param>
        /// <returns></returns>
        public Task<Models.BookingResponse> CancelDebit(string mediumId, Models.CancellationRequest cancellationRequest);
    }
}