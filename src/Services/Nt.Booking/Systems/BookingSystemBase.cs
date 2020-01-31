using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nt.Booking.Systems
{
    public abstract class BookingSystemBase
    {
        public NtBooking.BookingSystemType Type { get; protected set; }

        public virtual async Task<Models.InformationResponse> GetMediumInformation(string mediumId)
        {
            throw new System.NotImplementedException();
        }

        public virtual async Task<List<Models.InformationResponse>> GetMediumInformation() {
            throw new System.NotImplementedException();
        }

        public virtual async Task<Models.BookingResponse> Pay(Models.PaymentRequest paymentRequest)
        {
            throw new System.NotImplementedException();
        }

        public virtual async Task<Models.BookingResponse> Cancel(Models.CancellationRequest cancellationRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}
