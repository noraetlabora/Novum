using System;

namespace Nt.Booking.Systems.Access.ExSI
{
    public class ExSI : SystemBase
    {

        public override string CheckMedium(string mediumId)
        {
            throw new NotImplementedException();
        }
        public override string Cancel()
        {
            return this.name;
        }

        public override string Pay()
        {
            throw new NotImplementedException();
        }

    }
}
