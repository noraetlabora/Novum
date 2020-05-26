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
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IBookingSystem Create(ServerConfiguration configuration)
        {
            switch (configuration.BookingSystem)
            {
                case BookingSystemType.ExSI:
                    return new ExSI();
                case BookingSystemType.SVS:
                    var svs = new SVS(configuration);                    
                    return svs;
                default:
                    throw new Exception("Couldn't find a corresponding booking system for " + configuration.BookingSystem);
            }
        }
    }
}