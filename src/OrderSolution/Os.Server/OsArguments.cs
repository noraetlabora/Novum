using CommandLine;
using CommandLine.Text;
using System;
using System.Text;

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
        public string DatabaseIp { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [Option("dbPrt", Required = true, Default = "56773", HelpText = "database Port")]
        public uint DatabasePort { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [Option("dbNs", Required = true, Default = "USER", HelpText = "database Namespace")]
        public string DatabaseNamespace { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [Option("dbUsr", Required = true, HelpText = "database User")]
        public string DatabaseUser { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [Option("dbPwd", Required = true, HelpText = "database Password")]
        public string DatabasePassword { get; private set; }
        /// <summary>
        /// ip address where the OsServer runs
        /// </summary>
        /// <value></value>
        [Option("osIp", Required = true, Default = "127.0.0.1", HelpText = "Ordersolutions Server Ip")]
        public string OsServerIp { get; private set; }
        /// <summary>
        /// server port of OrderSolutions we can call for eg. printing
        /// in default.json flag "httpApiPort"
        /// </summary>
        [Option("osSPrt", Default = "1234", HelpText = "OrderSolutions Server Port")]
        public uint OsServerPort { get; private set; }
        /// <summary>
        /// client port of OrderSolutions who calls
        /// in default.json in flag servicePort"
        /// </summary>
        /// <value></value>
        [Option("osCPrt", Required = true, Default = "1234", HelpText = "OrderSolutions Client Port")]
        public uint OsClientPort { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("OsArguments: ").Append(System.Environment.NewLine);
            builder.Append("dbIp:   ").Append(DatabaseIp).Append(System.Environment.NewLine);
            builder.Append("dbPrt:  ").Append(DatabasePort).Append(System.Environment.NewLine);
            builder.Append("dbNs:   ").Append(DatabaseNamespace).Append(System.Environment.NewLine);
            builder.Append("dbUsr:  ").Append(DatabaseUser).Append(System.Environment.NewLine);
            if (DatabasePassword.Length > 0)
                builder.Append("dbPwd:  <set>").Append(System.Environment.NewLine);
            else
                builder.Append("dbPwd:  <not set>").Append(System.Environment.NewLine);
            builder.Append("osIp:   ").Append(OsServerIp).Append(System.Environment.NewLine);
            builder.Append("osSPrt: ").Append(OsServerPort).Append(System.Environment.NewLine);
            builder.Append("osCPrt: ").Append(OsClientPort).Append(System.Environment.NewLine);
            return builder.ToString();
        }
    }
}
