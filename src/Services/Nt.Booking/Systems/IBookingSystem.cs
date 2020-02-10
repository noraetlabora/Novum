using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nt.Booking.Systems
{
    public interface IBookingSystem
    {
        public NtBooking.BookingSystemType Type { get; set; }

        public Task<Models.InformationResponse> GetMediumInformation(string mediumId);

        public Task<List<Models.InformationResponse>> GetMediumInformation();

        public Task<Models.BookingResponse> Pay(Models.PaymentRequest paymentRequest);

        public Task<Models.BookingResponse> Cancel(Models.CancellationRequest cancellationRequest);
    }
}