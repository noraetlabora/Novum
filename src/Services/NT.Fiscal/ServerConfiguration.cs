namespace Nt.Fiscal
{
    public class ServerConfiguration
    {
        #region Service configuration

        public int Port { get; set; }

        #endregion Service configuration

        #region Methods

        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();
            builder.Append("Server Configuration: ");
            builder.Append("Service = NT.Fiscal|");
            builder.Append("Port = ").Append(Port.ToString());
            return builder.ToString();
        }

        #endregion Methods
    }
}