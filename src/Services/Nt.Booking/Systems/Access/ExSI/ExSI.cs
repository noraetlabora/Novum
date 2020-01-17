using System;

namespace Nt.Booking.Systems.Access.ExSI
{
    public class ExSI : SystemBase
    {

        public ExSI()
        {
            Partner = "ExSI";
        }

        public override string Cancel()
        {
            throw new NotImplementedException();
        }

        public override string CheckMedium(string mediumId)
        {
            throw new NotImplementedException();
        }

        public override string Pay()
        {
            throw new NotImplementedException();
        }
    }
}
