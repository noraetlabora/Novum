namespace Nt.Access
{
    /// <summary>
    /// 
    /// </summary>
    public class ServerConfiguration
    {
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

        public ServerConfiguration(string json)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();
            builder.Append("ServerConfiguration: Port=").Append(Port);
            return builder.ToString();
        }
    }
}
