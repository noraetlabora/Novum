
namespace Nt.Booking.Systems
{
    public abstract class SystemBase
    {
        public string Partner { get; protected set; }

        public abstract string CheckMedium(string mediumId);

        public abstract string Pay();

        public abstract string Cancel();
    }
}
