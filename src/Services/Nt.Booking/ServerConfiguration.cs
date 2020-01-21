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
        /// Port where the Nt.Booking Service is listening
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Address (Ip:Port, Uri, ..) of the Bookingsystem like "192.168.0.1:4000", "https://webservices-cert.storedvalue.com/svsxml/v1/services/SVSXMLWay", ...
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Username needed for booking system
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password needed for booking system
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Timeout in seconds
        /// </summary>
        public int Timeout { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public ServerConfiguration()
        {
            Port = 4711;
            Timeout = 10;
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
