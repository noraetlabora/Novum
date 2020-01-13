namespace Os.Server
{
    /// <summary>
    /// 
    /// </summary>
    public class ServerConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string DatabaseIp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public uint DatabasePort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string DatabaseNamespace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string DatabaseUser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string DatabasePassword { get; set; }
        /// <summary>
        /// port in default.json flag "posAdapterUrl"
        /// </summary>
        public uint OsServerPort { get; set; }
        /// <summary>
        /// ip address of Orderman OrderSolutions Server
        /// </summary>
        /// <value></value>
        public string OsClientIp { get; set; }
        /// <summary>
        /// port of Orderman OrderSolutions Server
        /// in default.json in flag httpApiPort"
        /// </summary>
        /// <value></value>
        public uint OsClientPort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ServerConfiguration()
        {
            DatabaseIp = "localhost";
            DatabasePort = 0;
            DatabaseNamespace = "";
            DatabaseUser = "";
            DatabasePassword = "";
            OsServerPort = 0;
            OsClientIp = "localhost";
            OsClientPort = 0;
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
            builder.Append("OsServerConfiguration: dbIp=").Append(DatabaseIp);
            builder.Append(", dbPrt=").Append(DatabasePort);
            builder.Append(", dbNs=").Append(DatabaseNamespace);
            builder.Append(", dbUsr=").Append(DatabaseUser);
            builder.Append(", dbPwd=");
            if (string.IsNullOrEmpty(DatabasePassword))
                builder.Append("<not set>");
            else
                builder.Append("<set>");
            builder.Append(", osSPrt=").Append(OsServerPort);
            builder.Append(", osCIp=").Append(OsClientIp);
            builder.Append(", osCPrt=").Append(OsClientPort);

            return builder.ToString();
        }
    }
}
