using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nt.Booking.Systems
{
    public abstract class BookingSystemBase
    {
        public NtBooking.BookingSystemType Type { get; protected set; }

        public virtual async Task<Models.InformationResponse> GetMediumInformation(string mediumId)
        {
            return new Models.InformationResponse();
        }

        public abstract List<Models.InformationResponse> GetMediumInformation();

        public abstract Models.BookingResponse Pay(Models.PaymentRequest paymentRequest);

        public abstract Models.BookingResponse Cancel(Models.CancellationRequest cancellationRequest);
    }
}
