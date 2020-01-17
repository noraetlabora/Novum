using System;

namespace Nt.Booking.Systems.Access.ExSI
{
    public class ExSI : SystemBase
    {

        public ExSI()
        {
            Partner = "ExSI";
        }
        public override Models.Information CheckMedium(string mediumId)
        {
            throw new NotImplementedException();
        }
        public override Models.Cancellation Cancel()
        {
            throw new NotImplementedException();
        }

        public override string Pay()
        {
            throw new NotImplementedException();
        }

    }
}
