using Nt.Booking.Systems.Access;
using Nt.Booking.Systems.Voucher.SVS;
using System;
using static Nt.Booking.NtBooking;

namespace Nt.Booking.Systems
{
    public static class BookingSystemFactory
    {
        public static IBookingSystem Create(ServerConfiguration configuration)
        {
            switch (configuration.BookingSystem)
            {
                case BookingSystemType.ExSI:
                    return new ExSI();
                case BookingSystemType.SVS:
                    var SVSLocation = "https://webservices-cert.storedvalue.com/svsxml/v1/services/SVSXMLWay";
                    var SVSTimeOut = 5;
                    var SVSUsername = "Username";
                    var SVSPassword = "secretPassword";
                    return new SVS(SVSLocation, SVSUsername, SVSPassword, SVSTimeOut);
                default:
                    throw new Exception("couldn't find a corresponding booking system for " + configuration.BookingSystem);
            }
        }
    }
}