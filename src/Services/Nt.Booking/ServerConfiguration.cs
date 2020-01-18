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
        public string BookingSystemName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint Port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>


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
            builder.Append(" System = ").Append(BookingSystemName);
            builder.Append(" Port =").Append(Port);
            return builder.ToString();
        }
    }
}
