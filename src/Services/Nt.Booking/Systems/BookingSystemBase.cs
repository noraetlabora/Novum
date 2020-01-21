using System.Collections.Generic;

namespace Nt.Booking.Systems
{
    public abstract class BookingSystemBase
    {
        public NtBooking.BookingSystemType BookingSystem { get; protected set; }

        public abstract Models.InformationResponse GetMediumInformation(string mediumId);

        public abstract List<Models.InformationResponse> GetMediumInformation();

        public abstract Models.BookingResponse Pay(Models.PaymentRequest paymentRequest);

        public abstract Models.BookingResponse Cancel(Models.CancellationRequest cancellationRequest);
    }
}
