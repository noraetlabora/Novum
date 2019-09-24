using CommandLine;

namespace Os.Server
{
    /// <summary>
    /// 
    /// </summary>
    public class OsArguments
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [Option("dbIp", Required = true, Default = "127.0.0.1", HelpText = "database Ip Address")]
        public string DatabaseIp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [Option("dbPrt", Required = true, Default = "56773", HelpText = "database Port")]
        public uint DatabasePort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [Option("dbNs", Required = true, Default = "USER", HelpText = "database Namespace")]
        public string DatabaseNamespace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [Option("dbUsr", Required = true, HelpText = "database User")]
        public string DatabaseUser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [Option("dbPwd", Required = true, HelpText = "database Password")]
        public string DatabasePassword { get; set; }
        /// <summary>
        /// port in default.json flag "posAdapterUrl"
        /// </summary>
        [Option("osSPrt", Default = "12346", HelpText = "server Port")]
        public uint OsServerPort { get; set; }
        /// <summary>
        /// ip address of Orderman OrderSolutions Server
        /// </summary>
        /// <value></value>
        [Option("osCIp", Required = true, Default = "localhost", HelpText = "OrderSolutions Client Ip")]
        public string OsClientIp { get; set; }
        /// <summary>
        /// port of Orderman OrderSolutions Server
        /// in default.json in flag httpApiPort"
        /// </summary>
        /// <value></value>
        [Option("osCPrt", Required = true, Default = "12344", HelpText = "OrderSolutions Client Port")]
        public uint OsClientPort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OsArguments()
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();
            builder.Append("OsArguments: dbIp=").Append(DatabaseIp);
            builder.Append(", dbPrt=").Append(DatabasePort);
            builder.Append(", dbNs=").Append(DatabaseNamespace);
            builder.Append(", dbUsr=").Append(DatabaseUser);
            builder.Append(", dbPwd=");
            if (string.IsNullOrEmpty(DatabasePassword))
                builder.Append("<not set>");
            else
                builder.Append("<set>");
            builder.Append(System.Environment.NewLine);
            builder.Append(", osSPrt=").Append(OsServerPort);
            builder.Append(", osCIp=").Append(OsClientIp);
            builder.Append(", osCPrt=").Append(OsClientPort);

            return builder.ToString();
        }
    }
}
