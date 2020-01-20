namespace Nt.Booking
{
    /// <summary>
    /// 
    /// </summary>
    public class ServerConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        public NtBooking.BookingSystemType BookingSystem { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint Port { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ServerConfiguration()
        {
            Port = 12345;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();
            builder.Append("ServerConfiguration: ");
            builder.Append(" BookingSystem = ").Append(BookingSystem);
            builder.Append(" Port =").Append(Port);
            return builder.ToString();
        }
    }
}
