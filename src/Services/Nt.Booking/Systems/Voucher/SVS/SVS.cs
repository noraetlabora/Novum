using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nt.Booking.Models;

namespace Nt.Booking.Systems.Voucher.SVS
{
    public class SVS : BookingSystemBase
    {
        public SVS() 
        {
            BookingSystem = NtBooking.BookingSystemType.SVS;
        }

        public override Models.Booking Cancel()
        {
            throw new NotImplementedException();
        }

        public override Information GetMediumInformation(string mediumId)
        {
            throw new NotImplementedException();
        }

        public override List<Information> GetMediumInformation()
        {
            throw new NotImplementedException();
        }

        public override Models.Booking Pay()
        {
            throw new NotImplementedException();
        }
    }
}
