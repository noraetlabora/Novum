using Nt.Booking.Systems.Access;
using Nt.Booking.Systems.Voucher.SVS;
using System;
using static Nt.Booking.NtBooking;

namespace Nt.Booking.Systems
{
    /// <summary>
    /// 
    /// </summary>
    public static class BookingSystemFactory
    {
        /// <summary>
        /// Factory that creates a new booking service.
        /// </summary>
        /// <param name="configuration">Basic service configuration.</param>
        /// <returns></returns>
        public static IBookingSystem Create(ServiceConfiguration configuration)
        {
            switch (configuration.BookingSystem)
            {
                case BookingSystemType.ExSI:
                    return new ExSI();
                case BookingSystemType.SVS:
                    var svsConfig = new SvsServiceConfiguration(configuration);
                    return new SVS(svsConfig);
                default:
                    throw new Exception(String.Format("Couldn't find a corresponding booking system for '{0}'.", configuration.BookingSystem));
            }
        }
    }
}