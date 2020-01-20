using System.Collections.Generic;

namespace Nt.Booking.Systems
{
    public abstract class BookingSystemBase
    {
        public NtBooking.BookingSystemType BookingSystem { get; protected set; }

        public abstract Models.Information GetMediumInformation(string mediumId);

        public abstract List<Models.Information> GetMediumInformation();

        public abstract Models.Booking Pay();

        public abstract Models.Booking Cancel();
    }
}
